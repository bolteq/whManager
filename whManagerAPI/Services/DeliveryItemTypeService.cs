using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using whManagerAPI.Models;
using whManagerLIB.Models;

namespace whManagerAPI.Services
{
    public interface IDeliveryItemTypeService
    {
        Task<DeliveryItemType> GetDeliveryItemType(int id);
        IQueryable<DeliveryItemType> GetDeliveryItemTypes();
        Task<DeliveryItemType> AddDeliveryItemType(DeliveryItemType deliveryItemType);
        Task<bool> DeleteDeliveryItemType(int id);
    }
    public class DeliveryItemTypeService : IDeliveryItemTypeService
    {
        private readonly WHManagerDbContext _context;
        public DeliveryItemTypeService(WHManagerDbContext context)
        {
            _context = context;
        }
        public async Task<DeliveryItemType> GetDeliveryItemType(int id)
        {
            var type = await _context
                            .DeliveryItemTypes
                            .FirstOrDefaultAsync(t => t.Id == id);

            return type;
        }

        public IQueryable<DeliveryItemType> GetDeliveryItemTypes()
        {
            var types = _context
                        .DeliveryItemTypes;

            return types;
        }

        public async Task<DeliveryItemType> AddDeliveryItemType(DeliveryItemType deliveryItemType)
        {

            bool bExists = await _context
                .DeliveryItemTypes
                .AnyAsync(dit => dit.Id == deliveryItemType.Id);

            if (bExists)
            {
                _context
                    .DeliveryItemTypes
                    .Update(deliveryItemType);

                await _context
                    .SaveChangesAsync();

                return deliveryItemType;
            }
            else
            {
                _context
                    .DeliveryItemTypes
                    .Add(deliveryItemType);

                await _context
                    .SaveChangesAsync();

                return deliveryItemType;
            }
        }

        public async Task<bool> DeleteDeliveryItemType(int id)
        {
            var type = await _context
                            .DeliveryItemTypes
                            .FirstOrDefaultAsync(dit => dit.Id == id);

            _context
                .DeliveryItemTypes
                .Remove(type);

            await _context
                .SaveChangesAsync();

            return true;
        }
    }
}
