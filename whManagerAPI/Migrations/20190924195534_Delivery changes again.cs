using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace whManagerAPI.Migrations
{
    public partial class Deliverychangesagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemTypeId",
                table: "Unloadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 9, 24, 21, 55, 33, 762, DateTimeKind.Local), "Zj059NWwX3G9O2nBQY57D8MRR6Ajata07okxLoYdZV8=", "4RVBF2bGK/sByQfEgR7HrFG1WLUPslZZLzlmCkjXE3M4aX2weLsbR4LXucrVJip5Qh04EN6FVwVP2u2ig87JNw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Unloadings_ItemTypeId",
                table: "Unloadings",
                column: "ItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Unloadings_DeliveryItemTypes_ItemTypeId",
                table: "Unloadings",
                column: "ItemTypeId",
                principalTable: "DeliveryItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unloadings_DeliveryItemTypes_ItemTypeId",
                table: "Unloadings");

            migrationBuilder.DropIndex(
                name: "IX_Unloadings_ItemTypeId",
                table: "Unloadings");

            migrationBuilder.DropColumn(
                name: "ItemTypeId",
                table: "Unloadings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 9, 24, 21, 34, 32, 149, DateTimeKind.Local), "ONiRB3No4wQPUbE13zlujsb02hAurRfCJS15Xca/3jQ=", "ikhyZlms2PjwC2QB/GDjUsrPXZWC5ZpJxqIpIkU5HcMzeVi66CqDblGThnhDhSUGms9aY9WzSHBoKHMw2kJyuA==" });
        }
    }
}
