using System.ComponentModel.DataAnnotations;

namespace whManagerLIB.Models
{
    public class Warehouse
    {
        [Key]
        public int warehouseId { get; set; }
        public string warehouseName { get; set; }
    }
}
