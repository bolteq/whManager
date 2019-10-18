using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace whManagerAPI.Migrations
{
    public partial class Carproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Capacity",
                table: "Cars",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "Cars",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 10, 18, 10, 18, 12, 937, DateTimeKind.Local), "DhcoDv3ckMch9+ZdN3JZL0q7kYB/8CW6139aqxEgrGA=", "JYH93pnVVSyDrxRO1Zyzyd/c36xrI3wpvswqsQV7mHrVOPPelL2m4jIMulHbnnksHn2G9DqiPtc/ekR0ro212A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Cars");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 9, 24, 22, 5, 55, 20, DateTimeKind.Local), "TGXHb8MYONJ2ZDqsVrraQyoGe3+01iNM4coyghmMMTA=", "hGfj0Qx5a5YU2oNTMgEnhXJLQDHQ7Uvi1YCDkQ25k4Jev2sTd7qIDuT34dWVvQgMvTTzSwchvnWm1cTntC0H4g==" });
        }
    }
}
