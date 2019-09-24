using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace whManagerAPI.Migrations
{
    public partial class Deliverychanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Trailers_TrailerId",
                table: "Deliveries");

            migrationBuilder.AlterColumn<int>(
                name: "TrailerId",
                table: "Deliveries",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 9, 24, 21, 34, 32, 149, DateTimeKind.Local), "ONiRB3No4wQPUbE13zlujsb02hAurRfCJS15Xca/3jQ=", "ikhyZlms2PjwC2QB/GDjUsrPXZWC5ZpJxqIpIkU5HcMzeVi66CqDblGThnhDhSUGms9aY9WzSHBoKHMw2kJyuA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Trailers_TrailerId",
                table: "Deliveries",
                column: "TrailerId",
                principalTable: "Trailers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Trailers_TrailerId",
                table: "Deliveries");

            migrationBuilder.AlterColumn<int>(
                name: "TrailerId",
                table: "Deliveries",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 9, 24, 21, 12, 39, 792, DateTimeKind.Local), "uvzY5VNMrbXvEmBmrTYfEDlQolvk4suktMamnqAj7eo=", "cBA/G1Y42zPwjmJqYkX03LZUpVxXvdk5N3bVVq6gqsH2EpnZha4olLyDfmrU3ZvCJoK3pW7PRQAw+eguwljQig==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Trailers_TrailerId",
                table: "Deliveries",
                column: "TrailerId",
                principalTable: "Trailers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
