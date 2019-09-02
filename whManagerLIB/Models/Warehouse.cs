using System.ComponentModel.DataAnnotations;

namespace whManagerLIB.Models
{
    public class Warehouse
    {
        [Key]
        public int warehouseId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
