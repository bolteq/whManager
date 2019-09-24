using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace whManagerAPI.Migrations
{
    public partial class Deliverychangesaaagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unloadings_Deliveries_DeliveryId",
                table: "Unloadings");

            migrationBuilder.DropForeignKey(
                name: "FK_Unloadings_DeliveryItems_DeliveryItemId",
                table: "Unloadings");

            migrationBuilder.DropForeignKey(
                name: "FK_Unloadings_DeliveryItemTypes_ItemTypeId",
                table: "Unloadings");

            migrationBuilder.DropIndex(
                name: "IX_Unloadings_DeliveryId",
                table: "Unloadings");

            migrationBuilder.DropIndex(
                name: "IX_Unloadings_DeliveryItemId",
                table: "Unloadings");

            migrationBuilder.DropIndex(
                name: "IX_Unloadings_ItemTypeId",
                table: "Unloadings");

            migrationBuilder.DropColumn(
                name: "DeliveryId",
                table: "Unloadings");

            migrationBuilder.DropColumn(
                name: "DeliveryItemId",
                table: "Unloadings");

            migrationBuilder.DropColumn(
                name: "ItemTypeId",
                table: "Unloadings");

            migrationBuilder.AddColumn<int>(
                name: "UnloadingId",
                table: "DeliveryItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 9, 24, 22, 5, 55, 20, DateTimeKind.Local), "TGXHb8MYONJ2ZDqsVrraQyoGe3+01iNM4coyghmMMTA=", "hGfj0Qx5a5YU2oNTMgEnhXJLQDHQ7Uvi1YCDkQ25k4Jev2sTd7qIDuT34dWVvQgMvTTzSwchvnWm1cTntC0H4g==" });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItems_UnloadingId",
                table: "DeliveryItems",
                column: "UnloadingId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryItems_Unloadings_UnloadingId",
                table: "DeliveryItems",
                column: "UnloadingId",
                principalTable: "Unloadings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryItems_Unloadings_UnloadingId",
                table: "DeliveryItems");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryItems_UnloadingId",
                table: "DeliveryItems");

            migrationBuilder.DropColumn(
                name: "UnloadingId",
                table: "DeliveryItems");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryId",
                table: "Unloadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryItemId",
                table: "Unloadings",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Unloadings_DeliveryId",
                table: "Unloadings",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Unloadings_DeliveryItemId",
                table: "Unloadings",
                column: "DeliveryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Unloadings_ItemTypeId",
                table: "Unloadings",
                column: "ItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Unloadings_Deliveries_DeliveryId",
                table: "Unloadings",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Unloadings_DeliveryItems_DeliveryItemId",
                table: "Unloadings",
                column: "DeliveryItemId",
                principalTable: "DeliveryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Unloadings_DeliveryItemTypes_ItemTypeId",
                table: "Unloadings",
                column: "ItemTypeId",
                principalTable: "DeliveryItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
