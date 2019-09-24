using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace whManagerAPI.Migrations
{
    public partial class Dataannotationchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Cars_CarId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Trailers_TrailerId",
                table: "Deliveries");

            migrationBuilder.AlterColumn<int>(
                name: "TrailerId",
                table: "Deliveries",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Deliveries",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Deliveries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 9, 24, 21, 12, 39, 792, DateTimeKind.Local), "uvzY5VNMrbXvEmBmrTYfEDlQolvk4suktMamnqAj7eo=", "cBA/G1Y42zPwjmJqYkX03LZUpVxXvdk5N3bVVq6gqsH2EpnZha4olLyDfmrU3ZvCJoK3pW7PRQAw+eguwljQig==" });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_UserId",
                table: "Deliveries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Cars_CarId",
                table: "Deliveries",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Trailers_TrailerId",
                table: "Deliveries",
                column: "TrailerId",
                principalTable: "Trailers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Users_UserId",
                table: "Deliveries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Cars_CarId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Trailers_TrailerId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Users_UserId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_UserId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Deliveries");

            migrationBuilder.AlterColumn<int>(
                name: "TrailerId",
                table: "Deliveries",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Deliveries",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 9, 24, 20, 23, 38, 886, DateTimeKind.Local), "D+pVXNEVk4ZIYmQlLarl/XEpu+oBwDBtK3PugN+cMYg=", "5uFNeO8UbSQFl2wh4tqGQj4z5BQAWrtqwdxxIFF8B0CqBx5j4+/nAEhzpgVrp+5OmPhPPam2/UwYdFllVoHqfA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Cars_CarId",
                table: "Deliveries",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Trailers_TrailerId",
                table: "Deliveries",
                column: "TrailerId",
                principalTable: "Trailers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
