using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace whManagerAPI.Models
{
    public class WarehouseModel
    {
        [Key]
        public int warehouseId { get; set; }
        public string warehouseName { get; set; }
    }
}
