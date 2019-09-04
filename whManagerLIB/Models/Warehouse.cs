using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace whManagerLIB.Models
{
    public class Warehouse
    {
        [Key]
        public int WarehouseId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<WarehouseWorkers> WarehouseWorkers { get; set; }
    }
}
