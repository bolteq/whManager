using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace whManagerAPI.Migrations
{
    public partial class Deliverychanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Deliveries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 9, 24, 20, 23, 38, 886, DateTimeKind.Local), "D+pVXNEVk4ZIYmQlLarl/XEpu+oBwDBtK3PugN+cMYg=", "5uFNeO8UbSQFl2wh4tqGQj4z5BQAWrtqwdxxIFF8B0CqBx5j4+/nAEhzpgVrp+5OmPhPPam2/UwYdFllVoHqfA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_CompanyId",
                table: "Deliveries",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Companies_CompanyId",
                table: "Deliveries",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Companies_CompanyId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_CompanyId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Deliveries");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 9, 24, 20, 10, 38, 254, DateTimeKind.Local), "r1kZZNn1IY1l+VNGaPH7L3nrT+J4F8da4jluq8dUsRU=", "a76+a3j+TIsWsPRzge8Ony7skqEZJ6w1mnye7eiIw2IGYLNKYWyvACkcpvOQ/xOw2HC5YuGnsTPh2HZqFr5uIQ==" });
        }
    }
}
