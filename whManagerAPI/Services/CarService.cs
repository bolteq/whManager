using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using whManagerAPI.Helpers;
using whManagerAPI.Models;
using whManagerLIB.Helpers;
using whManagerLIB.Models;

namespace whManagerAPI.Services
{
    public interface ICarService
    {
        Task<Car> GetCar(int id);

        Task<IList<Car>> GetCars();
        Task<Car> AddCar(Car car);
        Task<bool> DeleteCar(int id);
    }
    public class CarService : ICarService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly WHManagerDbContext _context;

        public CarService(IHttpContextAccessor httpContextAccessor, WHManagerDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        #region GetCar

        public async Task<Car> GetCar(int id)
        {
            //Sprawdź, czy użytkownik jest spedytorem
            bool isSpedytor = _httpContextAccessor
                            .HttpContext
                            .User
                            .Claims
                            .Any(c => c.Value == RoleHelper.Spedytor);

            //Pobierz rolę użytkownika
            var companyId = _httpContextAccessor.HttpContext.User.Claims
                            .Where(c => c.Type == MyClaims.CompanyId)
                            .Select(c => int.Parse(c.Value))
                            .FirstOrDefault();

            //Pobierz samochód z bazy danych
            var car = await _context
                            .Cars
                            .Include(c => c.Company)
                            .FirstOrDefaultAsync(c => c.Id == id);

            //Jeśli użytkownik jest spedytorem, a samochód nie należy od jego firmy, nie zwracaj nic.
            if (isSpedytor && car.companyId != companyId)
            {
                return null;
            }

            return car;

        }

        #endregion

        #region GetCars
        public async Task<IList<Car>> GetCars()
        {
            //Sprawdź, czy użytkownik jest spedytorem
            bool isSpedytor = _httpContextAccessor.HttpContext.User.Claims.Any(c => c.Value == RoleHelper.Spedytor);


            //Jeśli jest spedytorem, zwróc tylko samochody należącego do jego firmy, jeśli nie to wszystkie
            if (isSpedytor)
            {
                var companyId = _httpContextAccessor.HttpContext.User.Claims
                                .Where(c => c.Type == MyClaims.CompanyId)
                                .Select(c => int.Parse(c.Value))
                                .FirstOrDefault();

                var companyCars = _context
                                .Cars
                                .Include(c => c.Company)
                                .Where(c => c.companyId == companyId);

                return await companyCars.ToListAsync();
            }
            else
            {
                var allCars = _context
                    .Cars
                    .Include(c => c.Company);
                return await allCars.ToListAsync();
            }
        }

        #endregion

        #region AddCar
        public async Task<Car> AddCar(Car car)
        {
            var bExists = _context
                            .Cars
                            .Where(c => c.Id == car.Id)
                            .Any();

            var companyId = _httpContextAccessor
                            .HttpContext
                            .User
                            .Claims
                            .Where(c => c.Type == MyClaims.CompanyId)
                            .Select(c => int.Parse(c.Value))
                            .FirstOrDefault();

            if (bExists)
            {
                //Jeśli car istnieje, ale nie należy do firma użytkownika, zwróc null
                //W przeciwnym wypadku zezwól na modyfikację
                if (companyId != car.companyId)
                {
                    return null;
                }

                _context.Cars.Update(car);
                await _context.SaveChangesAsync();
                return car;
            }
            else
            {
                //Jeśli car nie istnieje, dodaj go do bazy danych,
                //Ustawiając companyId na companyId użytkownika z kontekstu
                car.companyId = companyId;

                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();

                return car;
            }
        }

        #endregion

        #region DeleteCar

        public async Task<bool> DeleteCar(int id)
        {

            bool isSpedytor = _httpContextAccessor
                            .HttpContext
                            .User
                            .Claims
                            .Any(c => c.Value == RoleHelper.Spedytor);

            var car = await _context
                            .Cars
                            .FirstOrDefaultAsync(c => c.Id == id);

            var companyId = _httpContextAccessor
                            .HttpContext
                            .User
                            .Claims
                            .Where(c => c.Type == MyClaims.CompanyId)
                            .Select(c => int.Parse(c.Value))
                            .FirstOrDefault();

            if (car == null)
            {
                return false;
            }

            //Jeśli użytkownik jest spedytorem i car nie jest z jego firmy, zwróc BadRequst
            if (isSpedytor && companyId != car.companyId)
            {
                return false;
            }

            _context
                .Cars
                .Remove(car);

            await _context.SaveChangesAsync();

            return true;
        }
        #endregion
    }
}
