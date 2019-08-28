using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace whManagerAPI.Models
{
    public class WHManagerDbContext : DbContext
    {
        public WHManagerDbContext(DbContextOptions<WHManagerDbContext> options)
            : base(options)
        { }

        public DbSet<WarehouseModel> Warehouses { get; set; }
    }
}
