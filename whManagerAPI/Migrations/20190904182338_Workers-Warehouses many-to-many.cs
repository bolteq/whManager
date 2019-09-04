using Microsoft.EntityFrameworkCore.Migrations;

namespace whManagerAPI.Migrations
{
    public partial class WorkersWarehousesmanytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "scheduleId",
                table: "WorkSchedules",
                newName: "ScheduleId");

            migrationBuilder.RenameColumn(
                name: "warehouseId",
                table: "Warehouses",
                newName: "WarehouseId");

            migrationBuilder.CreateTable(
                name: "WarehouseWorkers",
                columns: table => new
                {
                    WorkerId = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseWorkers", x => new { x.WorkerId, x.WarehouseId });
                    table.ForeignKey(
                        name: "FK_WarehouseWorkers_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseWorkers_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseWorkers_WarehouseId",
                table: "WarehouseWorkers",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehouseWorkers");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "WorkSchedules",
                newName: "scheduleId");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "Warehouses",
                newName: "warehouseId");
        }
    }
}
