using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace whManagerLIB.Models
{
    /// <summary>
    /// Klasa reprezentuje Magazyn
    /// </summary>
    public class Warehouse
    {
        /// <summary>
        /// Pole danych zawierające Id magazynu
        /// </summary>
        [Key]
        public int WarehouseId { get; set; }
        /// <summary>
        /// Pole danych nazwy magazynu
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Pole danych adresu magazynu
        /// </summary>
        public string Address { get; set; }
    }
}
