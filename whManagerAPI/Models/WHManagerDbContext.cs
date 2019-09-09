using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using whManagerLIB.Models;

namespace whManagerAPI.Models
{
    public class WHManagerDbContext : DbContext
    {
        public WHManagerDbContext(DbContextOptions<WHManagerDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Klucz główny złożony z dwóch atrybutów musi być tutaj zadeklarowany dla relacji wiele-do-wielu
            builder.Entity<WarehouseWorkers>().HasKey(t => new { t.WorkerId, t.WarehouseId });


            //DevOnly!
            builder.Entity<User>().HasData(
                new { Id = 1, EmailAddress = "admin@admin.net", PasswordHash = "admin", Role = "admin" });
        }

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<WarehouseWorkers> WarehouseWorkers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        
    }
}