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
    /// <summary>
    /// Serwis odpowiedzialny za operacje na obiektach DeliveryItemType w bazie danych
    /// </summary>
    public class DeliveryItemTypeService : IDeliveryItemTypeService
    {
        private readonly WHManagerDbContext _context;

        /// <summary>
        /// Konstruktor serwisu wstrzykujący zależności
        /// </summary>
        /// <param name="context">Klasa kontekstu bazy danych</param>
        public DeliveryItemTypeService(WHManagerDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Metoda pobierająca obiekt o przesłanym Id
        /// </summary>
        /// <param name="id">id obiektu</param>
        /// <returns>(awaitable) Pobrany obiekt DeliveryItemType</returns>
        public async Task<DeliveryItemType> GetDeliveryItemType(int id)
        {
            var type = await _context
                            .DeliveryItemTypes
                            .FirstOrDefaultAsync(t => t.Id == id);

            return type;
        }

        /// <summary>
        /// Metoda pobierająca obiekty z bazy danych
        /// </summary>
        /// <returns>IQueryable obiektów DeliveryItemType</returns>
        public IQueryable<DeliveryItemType> GetDeliveryItemTypes()
        {
            var types = _context
                        .DeliveryItemTypes;

            return types;
        }
        /// <summary>
        /// Dodaje/aktualizuje przesłany obiekt w bazie danych
        /// </summary>
        /// <param name="deliveryItemType">Obiekt do dodania/aktualizacji</param>
        /// <returns>Zaktualizowany obiekt typu DeliveryItemType</returns>
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
        /// <summary>
        /// Metoda usuwająca z bazy danych obiekt o przesłanym Id
        /// </summary>
        /// <param name="id">Id obiektu</param>
        /// <returns>True - sukces, False - niepowodzenie</returns>
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
