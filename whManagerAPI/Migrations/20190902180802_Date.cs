using Microsoft.EntityFrameworkCore.Migrations;

namespace whManagerAPI.Migrations
{
    public partial class Date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedule_Workers_workerId",
                table: "WorkSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkSchedule",
                table: "WorkSchedule");

            migrationBuilder.RenameTable(
                name: "WorkSchedule",
                newName: "WorkSchedules");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSchedule_workerId",
                table: "WorkSchedules",
                newName: "IX_WorkSchedules_workerId");

            migrationBuilder.AlterColumn<int>(
                name: "workerId",
                table: "WorkSchedules",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkSchedules",
                table: "WorkSchedules",
                column: "scheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedules_Workers_workerId",
                table: "WorkSchedules",
                column: "workerId",
                principalTable: "Workers",
                principalColumn: "workerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedules_Workers_workerId",
                table: "WorkSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkSchedules",
                table: "WorkSchedules");

            migrationBuilder.RenameTable(
                name: "WorkSchedules",
                newName: "WorkSchedule");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSchedules_workerId",
                table: "WorkSchedule",
                newName: "IX_WorkSchedule_workerId");

            migrationBuilder.AlterColumn<int>(
                name: "workerId",
                table: "WorkSchedule",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkSchedule",
                table: "WorkSchedule",
                column: "scheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedule_Workers_workerId",
                table: "WorkSchedule",
                column: "workerId",
                principalTable: "Workers",
                principalColumn: "workerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
