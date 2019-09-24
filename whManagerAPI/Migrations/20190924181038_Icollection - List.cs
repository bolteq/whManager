using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace whManagerAPI.Migrations
{
    public partial class IcollectionList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Deliveries_DeliveryId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Trailers_Deliveries_DeliveryId",
                table: "Trailers");

            migrationBuilder.DropIndex(
                name: "IX_Trailers_DeliveryId",
                table: "Trailers");

            migrationBuilder.DropIndex(
                name: "IX_Cars_DeliveryId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "DeliveryId",
                table: "Trailers");

            migrationBuilder.DropColumn(
                name: "DeliveryId",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Deliveries",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrailerId",
                table: "Deliveries",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 9, 24, 20, 10, 38, 254, DateTimeKind.Local), "r1kZZNn1IY1l+VNGaPH7L3nrT+J4F8da4jluq8dUsRU=", "a76+a3j+TIsWsPRzge8Ony7skqEZJ6w1mnye7eiIw2IGYLNKYWyvACkcpvOQ/xOw2HC5YuGnsTPh2HZqFr5uIQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_CarId",
                table: "Deliveries",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_TrailerId",
                table: "Deliveries",
                column: "TrailerId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Cars_CarId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Trailers_TrailerId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_CarId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_TrailerId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "TrailerId",
                table: "Deliveries");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryId",
                table: "Trailers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryId",
                table: "Cars",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 9, 24, 17, 14, 18, 106, DateTimeKind.Local), "K6o3WBOLhXilWuRVDK+3Tpog3d7znIff53o9G92UTuE=", "T9fCbgvy4Wzq0gyGlvsTltV9qnaDj+Gs0fusp4X6KgPNdgCGu6tP5tgpPbYSFvA/bhIPHYBAUwuxxbPOCP7jBw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_DeliveryId",
                table: "Trailers",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DeliveryId",
                table: "Cars",
                column: "DeliveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Deliveries_DeliveryId",
                table: "Cars",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trailers_Deliveries_DeliveryId",
                table: "Trailers",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
