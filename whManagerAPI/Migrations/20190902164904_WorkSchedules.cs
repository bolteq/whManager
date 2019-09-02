using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace whManagerAPI.Migrations
{
    public partial class WorkSchedules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "warehouseName",
                table: "Warehouses",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Warehouses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkSchedule",
                columns: table => new
                {
                    scheduleId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TimeStart = table.Column<DateTime>(type: "Date", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "Date", nullable: false),
                    workerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSchedule", x => x.scheduleId);
                    table.ForeignKey(
                        name: "FK_WorkSchedule_Workers_workerId",
                        column: x => x.workerId,
                        principalTable: "Workers",
                        principalColumn: "workerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedule_workerId",
                table: "WorkSchedule",
                column: "workerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkSchedule");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Warehouses");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Warehouses",
                newName: "warehouseName");
        }
    }
}
