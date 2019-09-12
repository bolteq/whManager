using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace whManagerAPI.Migrations
{
    public partial class IdentitychangesAddeddefaultadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "EmailAddress", "PasswordHash", "PasswordSalt", "Role", "Token" },
                values: new object[] { 1, new DateTime(2019, 9, 12, 19, 52, 29, 222, DateTimeKind.Local), "admin@admin.net", "51a6kaU3lBHqcavCyp7NfC++Jqe9dd090mpHyjG1GbU=", "zuBhdrEaxckan3PPNfMgZqdKrF9guIkOIpP/ZvFSmC6Jhk7bgoiLFsysvSgJmYsRPtLkv/YrPvz0nv78dSY8ng==", "Administrator", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
