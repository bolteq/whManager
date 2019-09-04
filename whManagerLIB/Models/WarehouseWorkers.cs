using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace whManagerLIB.Models
{
    public class WarehouseWorkers
    {
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
