using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using whManagerAPI.Helpers;
using whManagerLIB.Helpers;
using whManagerLIB.Models;

namespace whManagerAPI.Models
{
    /// <summary>
    /// Klasa inicjalizująca bazę danych (Entity Framework Core)
    /// </summary>
    public class WHManagerDbContext : DbContext
    {
        private readonly PasswordCrypter _passwordCrypter;

        /// <summary>
        /// Konstruktor klasy WHManagerDbContext
        /// </summary>
        /// <param name="options">Opcje z Startup.cs</param>
        /// <param name="passwordCrypter">Serwis odpowiedzialny za szyfrowanie haseł</param>
        public WHManagerDbContext(DbContextOptions<WHManagerDbContext> options, PasswordCrypter passwordCrypter)
            : base(options)
        {
            _passwordCrypter = passwordCrypter;
        }
        /// <summary>
        /// Reprezentuje tabelę zawierającą obiekty typu Warehouse w bazie danych
        /// </summary>
        public DbSet<Warehouse> Warehouses { get; set; }
        /// <summary>
        /// Reprezentuje tabelę zawierającą obiekty typu User w bazie danych
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Reprezentuje tabelę zawierającą obiekty typu Role w bazie danych
        /// </summary>
        public DbSet<Role> Roles { get; set; }
        /// <summary>
        /// Reprezentuje tabelę zawierającą obiekty typu Delivery w bazie danych
        /// </summary>
        public DbSet<Delivery> Deliveries { get; set; }
        /// <summary>
        /// Reprezentuje tabelę zawierającą obiekty typu DeliveryItem w bazie danych
        /// </summary>
        public DbSet<DeliveryItem> DeliveryItems { get; set; }
        /// <summary>
        /// Reprezentuje tabelę zawierającą obiekty typu DeliveryItemType w bazie danych
        /// </summary>
        public DbSet<DeliveryItemType> DeliveryItemTypes { get; set; }

        /// <summary>
        /// Reprezentuje tabelę zawierającą obiekty typu Unloading w bazie danych
        /// </summary>
        public DbSet<Unloading> Unloadings { get; set; }

        /// <summary>
        /// Reprezentuje tabelę zawierającą obiekty typu Car w bazie danych
        /// </summary>
        public DbSet<Car> Cars { get; set; }

        /// <summary>
        /// Reprezentuje tabelę zawierającą obiekty typu Company w bazie danych
        /// </summary>
        public DbSet<Company> Companies { get; set; }

        /// <summary>
        /// Reprezentuje tabelę zawierającą obiekty typu Trailer w bazie danych
        /// </summary>
        public DbSet<Trailer> Trailers { get; set; }


        /// <summary>
        /// Nadpisuje bazową funkcję EFCore tworzącą bazę danych,
        /// dodaje przykładowe dane.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {

            //Na cele deweloperskie oraz prezentacji!

            var adminSalt = _passwordCrypter.CreateSalt();
            builder.Entity<User>().HasData(
                new { Id = 1,
                    EmailAddress = "admin@admin.net",
                    Name = "Dawid",
                    Surname = "Czyżycki",
                    PhoneNumber = "+48535561160",
                    PasswordSalt = adminSalt,
                    PasswordHash = _passwordCrypter.CreateHash("admin", adminSalt),
                    Role = RoleHelper.Administrator,
                    DateCreated = DateTime.Now,
                    CompanyId = 1
                }) ;

            builder.Entity<Company>().HasData(
                new
                {
                    Id = 1,
                    Name = "BTH Import Stal"
                });

            //Przykładowa dostawa

            //builder.Entity<Delivery>().HasData(
            //    new Delivery
            //    {
            //        Id = 1,
            //        CarId = 36,
            //        CompanyId = 1,
            //        DeliveryFrom = "Tarnów",
            //        TrailerId = null,
            //        DeliveryItems = new List<DeliveryItem>
            //        {
            //            new DeliveryItem
            //            {
            //                Id = 1,
            //                Count = 1000,
            //                ItemTypeId = 1,
            //                DeliveryId = 1,
            //            }
            //        },
            //        Unloadings = new List<Unloading>
            //        {
            //            new Unloading
            //            {
            //                DeliveryId = 1,
            //                TimeStart = DateTime.Now,
            //                TimeEnd = DateTime.Now.AddDays(1),
            //                DeliveryItemId = 1,
            //            }
            //        }
            //    }) ;
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

        
    }
}