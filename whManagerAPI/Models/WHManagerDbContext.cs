using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using whManagerAPI.Helpers;
using whManagerLIB.Models;

namespace whManagerAPI.Models
{
    public class WHManagerDbContext : DbContext
    {
        private readonly PasswordCrypter _passwordCrypter;

        public WHManagerDbContext(DbContextOptions<WHManagerDbContext> options, PasswordCrypter passwordCrypter)
            : base(options)
        {
            _passwordCrypter = passwordCrypter;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //Na cele deweloperskie oraz prezentacji!

            var adminSalt = _passwordCrypter.CreateSalt();
            builder.Entity<User>().HasData(
                new { Id = 1,
                    EmailAddress = "admin@admin.net",
                    PasswordSalt = adminSalt,
                    PasswordHash = _passwordCrypter.CreateHash("admin", adminSalt),
                    Role = RoleHelper.Administrator,
                    DateCreated = DateTime.Now });


            //Podstawowe role
            builder.Entity<Role>().HasData(
                new {
                    Name = RoleHelper.Kierowca,
                    Description = "Rola przeznaczona dla kierowców danego spedytora, pozwala tylko na logowanie się i przeglądanie własnych dostaw" },
                new
                {
                    Name = RoleHelper.Spedytor,
                    Description = "Rola przeznaczona dla spedytorów, pozwala na: tworzenie użytkowników z uprawnieniami kierowców, dodawanie i" +
                " przeglądanie własnych samochodów i dostaw, rezerwacje terminów"
                },
                new
                {
                    Name = RoleHelper.Magazynier,
                    Description = "Wszystkie uprawnienia poza tworzeniem nowych użytkowników"
                },
                new
                {
                    Name = RoleHelper.Administrator,
                    Description = "Wszystkie uprawnienia"
                }
                );
        }

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        
    }
}