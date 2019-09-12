using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace whManagerAPI.Migrations
{
    public partial class IdentitychangesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 9, 12, 19, 53, 50, 608, DateTimeKind.Local), "NcdSH9v26HxU+E055hA2NFqsxgOl/RlRlrBm09MeMn8=", "mhIqy+de5cQ4/fRk7Jrl7hg1C6JeT6RVlK6axgzJxFLjAcbBcs1AV/S4/FqWnPAufHuZNm9s8Q7Qzc3wOrtMVA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 9, 12, 19, 52, 29, 222, DateTimeKind.Local), "51a6kaU3lBHqcavCyp7NfC++Jqe9dd090mpHyjG1GbU=", "zuBhdrEaxckan3PPNfMgZqdKrF9guIkOIpP/ZvFSmC6Jhk7bgoiLFsysvSgJmYsRPtLkv/YrPvz0nv78dSY8ng==" });
        }
    }
}
