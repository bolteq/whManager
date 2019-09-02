using Microsoft.EntityFrameworkCore.Migrations;

namespace whManagerAPI.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedules_Workers_workerId",
                table: "WorkSchedules");

            migrationBuilder.RenameColumn(
                name: "workerId",
                table: "WorkSchedules",
                newName: "WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSchedules_workerId",
                table: "WorkSchedules",
                newName: "IX_WorkSchedules_WorkerId");

            migrationBuilder.RenameColumn(
                name: "workerId",
                table: "Workers",
                newName: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedules_Workers_WorkerId",
                table: "WorkSchedules",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "WorkerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedules_Workers_WorkerId",
                table: "WorkSchedules");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "WorkSchedules",
                newName: "workerId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSchedules_WorkerId",
                table: "WorkSchedules",
                newName: "IX_WorkSchedules_workerId");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "Workers",
                newName: "workerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedules_Workers_workerId",
                table: "WorkSchedules",
                column: "workerId",
                principalTable: "Workers",
                principalColumn: "workerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
