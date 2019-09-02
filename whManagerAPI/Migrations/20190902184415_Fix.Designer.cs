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
    [Migration("20190902184415_Fix")]
    partial class Fix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("whManagerLIB.Models.Warehouse", b =>
                {
                    b.Property<int>("warehouseId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Name");

                    b.HasKey("warehouseId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("whManagerLIB.Models.Worker", b =>
                {
                    b.Property<int>("WorkerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddress");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Surname");

                    b.HasKey("WorkerId");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("whManagerLIB.Models.WorkSchedule", b =>
                {
                    b.Property<int>("scheduleId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("TimeEnd")
                        .HasColumnType("Date");

                    b.Property<DateTime>("TimeStart")
                        .HasColumnType("Date");

                    b.Property<int>("WorkerId");

                    b.HasKey("scheduleId");

                    b.HasIndex("WorkerId");

                    b.ToTable("WorkSchedules");
                });

            modelBuilder.Entity("whManagerLIB.Models.WorkSchedule", b =>
                {
                    b.HasOne("whManagerLIB.Models.Worker", "Worker")
                        .WithMany("WorkSchedules")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
