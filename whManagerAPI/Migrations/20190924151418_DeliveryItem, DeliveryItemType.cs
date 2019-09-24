using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace whManagerAPI.Migrations
{
    public partial class DeliveryItemDeliveryItemType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unloadings_DeliveryTypes_DeliveryTypeId",
                table: "Unloadings");

            migrationBuilder.DropTable(
                name: "DeliveryTypes");

            migrationBuilder.RenameColumn(
                name: "DeliveryTypeId",
                table: "Unloadings",
                newName: "DeliveryItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Unloadings_DeliveryTypeId",
                table: "Unloadings",
                newName: "IX_Unloadings_DeliveryItemId");

            migrationBuilder.CreateTable(
                name: "DeliveryItemTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryItemTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ItemTypeId = table.Column<int>(nullable: false),
                    Count = table.Column<float>(nullable: false),
                    DeliveryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryItems_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryItems_DeliveryItemTypes_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "DeliveryItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 9, 24, 17, 14, 18, 106, DateTimeKind.Local), "K6o3WBOLhXilWuRVDK+3Tpog3d7znIff53o9G92UTuE=", "T9fCbgvy4Wzq0gyGlvsTltV9qnaDj+Gs0fusp4X6KgPNdgCGu6tP5tgpPbYSFvA/bhIPHYBAUwuxxbPOCP7jBw==" });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItems_DeliveryId",
                table: "DeliveryItems",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItems_ItemTypeId",
                table: "DeliveryItems",
                column: "ItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Unloadings_DeliveryItems_DeliveryItemId",
                table: "Unloadings",
                column: "DeliveryItemId",
                principalTable: "DeliveryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unloadings_DeliveryItems_DeliveryItemId",
                table: "Unloadings");

            migrationBuilder.DropTable(
                name: "DeliveryItems");

            migrationBuilder.DropTable(
                name: "DeliveryItemTypes");

            migrationBuilder.RenameColumn(
                name: "DeliveryItemId",
                table: "Unloadings",
                newName: "DeliveryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Unloadings_DeliveryItemId",
                table: "Unloadings",
                newName: "IX_Unloadings_DeliveryTypeId");

            migrationBuilder.CreateTable(
                name: "DeliveryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DeliveryId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryTypes_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2019, 9, 21, 14, 59, 53, 600, DateTimeKind.Local), "CD0fgZW+TuR28YgA7yqBtL6pCYNEau0IaLP2GbgD7Fc=", "I38i82RoymmhllUvA4ZQrdr/mkBVKbKpdrxNLoDBJ8t4YU1949TVmCo9uYaCpijLDT61FPxGDdLkf1+fiZjmsw==" });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryTypes_DeliveryId",
                table: "DeliveryTypes",
                column: "DeliveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Unloadings_DeliveryTypes_DeliveryTypeId",
                table: "Unloadings",
                column: "DeliveryTypeId",
                principalTable: "DeliveryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
