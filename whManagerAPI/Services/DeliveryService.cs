using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using whManagerAPI.Helpers;
using whManagerAPI.Models;
using whManagerLIB.Helpers;
using whManagerLIB.Models;

namespace whManagerAPI.Services
{
    public interface IDeliveryService
    {
        Task<Delivery> GetDelivery(int id);
        IQueryable<Delivery> GetDeliveries();
        Task<Delivery> SetDelivery(Delivery delivery);
        Task<bool> DeleteDelivery(int id);

    }
    public class DeliveryService : IDeliveryService
    {
        private readonly WHManagerDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeliveryService(WHManagerDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #region GetDelivery
        public async Task<Delivery> GetDelivery(int id)
        {
            bool isSpedytor = _httpContextAccessor
                                .HttpContext
                                .User
                                .Claims
                                .Any(c => c.Value == RoleHelper.Spedytor);

            bool isKierowca = _httpContextAccessor
                                .HttpContext
                                .User
                                .Claims
                                .Any(c => c.Value == RoleHelper.Kierowca);

            var companyId = _httpContextAccessor
                                .HttpContext.User.Claims
                                .Where(c => c.Type == MyClaims.CompanyId)
                                .Select(c => int.Parse(c.Value))
                                .FirstOrDefault();

            var username = _httpContextAccessor
                                .HttpContext.User.Claims
                                .Where(c => c.Type == ClaimTypes.Name)
                                .Select(c => c.Value)
                                .FirstOrDefault();

            var delivery = await _context
                                .Deliveries
                                .Include(d => d.User)
                                .Include(d => d.Car)
                                .Include(d => d.Trailer)
                                .Include(d => d.Company)
                                .Include(d => d.DeliveryItems).ThenInclude(di => di.ItemType)
                                .FirstOrDefaultAsync(d => d.Id == id);

            //Jeśli użytkownik jest spedytorem, a dostawa nie należy do jego firmy nie zwracaj nic
            if (isSpedytor && delivery.CompanyId != companyId) return null;

            //Jeśli użytkownik jest kierowcą, i nie dostarcza dostawy, którą chce pobrać, nie zwracaj nic
            if (isKierowca && delivery.User.EmailAddress != username) return null;

            return delivery;
        }
        #endregion

        #region GetDeliveries
        public IQueryable<Delivery> GetDeliveries()
        {
            bool isSpedytor = _httpContextAccessor
                            .HttpContext
                            .User
                            .Claims
                            .Any(c => c.Value == RoleHelper.Spedytor);

            var companyId = _httpContextAccessor
                            .HttpContext
                            .User
                            .Claims
                            .Where(c => c.Type == MyClaims.CompanyId)
                            .Select(c => int.Parse(c.Value))
                            .FirstOrDefault();

            switch (isSpedytor)
            {
                case true:
                    var companyDeliveries = _context
                        .Deliveries
                        .Where(d => d.CompanyId == companyId)
                            .Include(d => d.Car)
                            .Include(d => d.Trailer)
                            .Include(d => d.Company)
                            .Include(d => d.User)
                            .Include(d => d.DeliveryItems);


                    return companyDeliveries;
                case false:
                    var allDeliveries = _context
                        .Deliveries
                        .Include(d => d.Car)
                        .Include(d => d.Trailer)
                        .Include(d => d.Company)
                        .Include(d => d.User)
                        .Include(d => d.DeliveryItems);

                    return allDeliveries;
            }

            return null;
        }
        #endregion

        #region SetDelivery
        public async Task<Delivery> SetDelivery(Delivery delivery)
        {
            var companyId = _httpContextAccessor
                            .HttpContext
                            .User
                            .Claims
                            .Where(c => c.Type == MyClaims.CompanyId)
                            .Select(c => int.Parse(c.Value))
                            .FirstOrDefault();

            bool bExists = await _context
                            .Deliveries
                            .AnyAsync(d => d.Id == delivery.Id);

            switch (bExists)
            {
                case true:
                    //Jesli delivery istnieje, ale nie należy do firmy użytkownika, zwróć null
                    if (delivery.CompanyId != companyId) return null;

                    //Aktualizuj delivery, zapisz zmiany i zwróć
                    _context
                        .Deliveries
                        .Update(delivery);

                    await _context.SaveChangesAsync();
                    return delivery;

                case false:
                    delivery.CompanyId = companyId;
                    _context.Deliveries.Add(delivery);

                    await _context.SaveChangesAsync();
                    return delivery;
            }

            return null;
        }
        #endregion

        #region DeleteDelivery
        public async Task<bool> DeleteDelivery(int id)
        {

            bool isSpedytor = _httpContextAccessor
                            .HttpContext
                            .User
                            .Claims
                            .Any(c => c.Value == RoleHelper.Spedytor);

            var delivery = await _context
                            .Deliveries
                            .FirstOrDefaultAsync(c => c.Id == id);

            var companyId = _httpContextAccessor
                            .HttpContext
                            .User
                            .Claims
                            .Where(c => c.Type == MyClaims.CompanyId)
                            .Select(c => int.Parse(c.Value))
                            .FirstOrDefault();

            if (delivery == null) return false;

            //Jeśli użytkownik jest spedytorem i delivery nie jest z jego firmy, zwróc false
            if (isSpedytor && companyId != delivery.CompanyId) return false;

            _context
                .Deliveries
                .Remove(delivery);

            await _context.SaveChangesAsync();

            return true;
        }
        #endregion

    }
}
