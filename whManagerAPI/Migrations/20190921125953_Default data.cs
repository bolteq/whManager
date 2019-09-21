using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace whManagerAPI.Migrations
{
    public partial class Defaultdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { 1, null, "BTH Import Stal" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CompanyId", "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { 1, new DateTime(2019, 9, 21, 14, 59, 53, 600, DateTimeKind.Local), "CD0fgZW+TuR28YgA7yqBtL6pCYNEau0IaLP2GbgD7Fc=", "I38i82RoymmhllUvA4ZQrdr/mkBVKbKpdrxNLoDBJ8t4YU1949TVmCo9uYaCpijLDT61FPxGDdLkf1+fiZjmsw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CompanyId", "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { null, new DateTime(2019, 9, 21, 14, 42, 38, 601, DateTimeKind.Local), "5+r81qCZ8EwX5oVA9C/NQ6/qRtKd+Vb7ty+v7eHn87o=", "5TM7/kxV8uUhMVgrglyCz76L7Cmxg1/zJGM8nW9L5AC0HbY/5IkZuXnF2YmbtVVgx/4ppWYvDEW+Sb+q3jfiwQ==" });
        }
    }
}
