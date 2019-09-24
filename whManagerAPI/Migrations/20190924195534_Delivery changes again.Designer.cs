﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using whManagerAPI.Models;

namespace whManagerAPI.Migrations
{
    [DbContext(typeof(WHManagerDbContext))]
    [Migration("20190924195534_Delivery changes again")]
    partial class Deliverychangesagain
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("whManagerLIB.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PlateNumber");

                    b.Property<int>("companyId");

                    b.HasKey("Id");

                    b.HasIndex("companyId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("whManagerLIB.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new { Id = 1, Name = "BTH Import Stal" }
                    );
                });

            modelBuilder.Entity("whManagerLIB.Models.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CarId");

                    b.Property<int>("CompanyId");

                    b.Property<string>("DeliveryFrom");

                    b.Property<int?>("TrailerId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("TrailerId");

                    b.HasIndex("UserId");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("whManagerLIB.Models.DeliveryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Count");

                    b.Property<int>("DeliveryId");

                    b.Property<int>("ItemTypeId");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("ItemTypeId");

                    b.ToTable("DeliveryItems");
                });

            modelBuilder.Entity("whManagerLIB.Models.DeliveryItemType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("DeliveryItemTypes");
                });

            modelBuilder.Entity("whManagerLIB.Models.Role", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Name");

                    b.ToTable("Roles");

                    b.HasData(
                        new { Name = "Kierowca", Description = "Rola przeznaczona dla kierowców danego spedytora, pozwala tylko na logowanie się i przeglądanie własnych dostaw" },
                        new { Name = "Spedytor", Description = "Rola przeznaczona dla spedytorów, pozwala na: tworzenie użytkowników z uprawnieniami kierowców, dodawanie i przeglądanie własnych samochodów i dostaw, rezerwacje terminów" },
                        new { Name = "Magazynier", Description = "Wszystkie uprawnienia poza tworzeniem nowych użytkowników" },
                        new { Name = "Administrator", Description = "Wszystkie uprawnienia" }
                    );
                });

            modelBuilder.Entity("whManagerLIB.Models.Trailer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<string>("PlateNumers");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Trailers");
                });

            modelBuilder.Entity("whManagerLIB.Models.Unloading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DeliveryId");

                    b.Property<int>("DeliveryItemId");

                    b.Property<int>("ItemTypeId");

                    b.Property<DateTime>("TimeEnd");

                    b.Property<DateTime>("TimeStart");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("DeliveryItemId");

                    b.HasIndex("ItemTypeId");

                    b.ToTable("Unloadings");
                });

            modelBuilder.Entity("whManagerLIB.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CompanyId");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("EmailAddress")
                        .IsRequired();

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("PasswordSalt");

                    b.Property<string>("Role");

                    b.Property<string>("Token");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Users");

                    b.HasData(
                        new { Id = 1, CompanyId = 1, DateCreated = new DateTime(2019, 9, 24, 21, 55, 33, 762, DateTimeKind.Local), EmailAddress = "admin@admin.net", PasswordHash = "Zj059NWwX3G9O2nBQY57D8MRR6Ajata07okxLoYdZV8=", PasswordSalt = "4RVBF2bGK/sByQfEgR7HrFG1WLUPslZZLzlmCkjXE3M4aX2weLsbR4LXucrVJip5Qh04EN6FVwVP2u2ig87JNw==", Role = "Administrator" }
                    );
                });

            modelBuilder.Entity("whManagerLIB.Models.Warehouse", b =>
                {
                    b.Property<int>("WarehouseId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Name");

                    b.HasKey("WarehouseId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("whManagerLIB.Models.Car", b =>
                {
                    b.HasOne("whManagerLIB.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("companyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("whManagerLIB.Models.Delivery", b =>
                {
                    b.HasOne("whManagerLIB.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("whManagerLIB.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("whManagerLIB.Models.Trailer", "Trailer")
                        .WithMany()
                        .HasForeignKey("TrailerId");

                    b.HasOne("whManagerLIB.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("whManagerLIB.Models.DeliveryItem", b =>
                {
                    b.HasOne("whManagerLIB.Models.Delivery", "Delivery")
                        .WithMany("DeliveryItems")
                        .HasForeignKey("DeliveryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("whManagerLIB.Models.DeliveryItemType", "ItemType")
                        .WithMany()
                        .HasForeignKey("ItemTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("whManagerLIB.Models.Trailer", b =>
                {
                    b.HasOne("whManagerLIB.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("whManagerLIB.Models.Unloading", b =>
                {
                    b.HasOne("whManagerLIB.Models.Delivery", "Delivery")
                        .WithMany("Unloadings")
                        .HasForeignKey("DeliveryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("whManagerLIB.Models.DeliveryItem", "DeliveryItem")
                        .WithMany()
                        .HasForeignKey("DeliveryItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("whManagerLIB.Models.DeliveryItemType", "ItemType")
                        .WithMany()
                        .HasForeignKey("ItemTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("whManagerLIB.Models.User", b =>
                {
                    b.HasOne("whManagerLIB.Models.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId");
                });
#pragma warning restore 612, 618
        }
    }
}
