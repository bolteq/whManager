﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using whManagerAPI.Models;

namespace whManagerAPI.Migrations
{
    [DbContext(typeof(WHManagerDbContext))]
    partial class WHManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("warehouseName");

                    b.HasKey("warehouseId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("whManagerLIB.Models.Worker", b =>
                {
                    b.Property<int>("workerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.HasKey("workerId");

                    b.ToTable("Workers");
                });
#pragma warning restore 612, 618
        }
    }
}
