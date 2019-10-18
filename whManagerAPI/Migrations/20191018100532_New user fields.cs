using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace whManagerAPI.Migrations
{
    public partial class Newuserfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Users",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 10, 18, 12, 5, 31, 729, DateTimeKind.Local), "OoZ7QbD1sl4D6MQSfmblAwP/8yavwsiuBIF3taMo5bw=", "MdB4xbSxDuAQRoHKmi2eE4F1p+VPP2HAoTGck80ktQalcvZrRl6TxY6orwU8YrhXCf0oNp1clLJ355J7JtJOkg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 10, 18, 10, 18, 12, 937, DateTimeKind.Local), "DhcoDv3ckMch9+ZdN3JZL0q7kYB/8CW6139aqxEgrGA=", "JYH93pnVVSyDrxRO1Zyzyd/c36xrI3wpvswqsQV7mHrVOPPelL2m4jIMulHbnnksHn2G9DqiPtc/ekR0ro212A==" });
        }
    }
}
