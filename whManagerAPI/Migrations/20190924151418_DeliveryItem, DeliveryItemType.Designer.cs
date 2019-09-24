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
    [Migration("20190924151418_DeliveryItem, DeliveryItemType")]
    partial class DeliveryItemDeliveryItemType
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

                    b.Property<int?>("DeliveryId");

                    b.Property<string>("PlateNumber");

                    b.Property<int>("companyId");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryId");

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

                    b.Property<string>("DeliveryFrom");

                    b.HasKey("Id");

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

                    b.Property<int?>("DeliveryId");

                    b.Property<string>("PlateNumers");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("DeliveryId");

                    b.ToTable("Trailers");
                });

            modelBuilder.Entity("whManagerLIB.Models.Unloading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DeliveryId");

                    b.Property<int>("DeliveryItemId");

                    b.Property<DateTime>("TimeEnd");

                    b.Property<DateTime>("TimeStart");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("DeliveryItemId");

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
                        new { Id = 1, CompanyId = 1, DateCreated = new DateTime(2019, 9, 24, 17, 14, 18, 106, DateTimeKind.Local), EmailAddress = "admin@admin.net", PasswordHash = "K6o3WBOLhXilWuRVDK+3Tpog3d7znIff53o9G92UTuE=", PasswordSalt = "T9fCbgvy4Wzq0gyGlvsTltV9qnaDj+Gs0fusp4X6KgPNdgCGu6tP5tgpPbYSFvA/bhIPHYBAUwuxxbPOCP7jBw==", Role = "Administrator" }
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
                    b.HasOne("whManagerLIB.Models.Delivery")
                        .WithMany("Cars")
                        .HasForeignKey("DeliveryId");

                    b.HasOne("whManagerLIB.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("companyId")
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

                    b.HasOne("whManagerLIB.Models.Delivery")
                        .WithMany("Trailers")
                        .HasForeignKey("DeliveryId");
                });

            modelBuilder.Entity("whManagerLIB.Models.Unloading", b =>
                {
                    b.HasOne("whManagerLIB.Models.Delivery", "Delivery")
                        .WithMany()
                        .HasForeignKey("DeliveryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("whManagerLIB.Models.DeliveryItem", "DeliveryItem")
                        .WithMany()
                        .HasForeignKey("DeliveryItemId")
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