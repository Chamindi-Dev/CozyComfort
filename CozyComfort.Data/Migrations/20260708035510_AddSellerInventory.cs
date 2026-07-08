using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyComfort.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSellerInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SellerInventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    BlanketModelId = table.Column<int>(type: "int", nullable: false),
                    QuantityOnHand = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ReservedQuantity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: false, computedColumnSql: "[QuantityOnHand]-[ReservedQuantity]", stored: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerInventories", x => x.Id);
                    table.CheckConstraint("CK_SellerInventories_Qty", "[QuantityOnHand] >= 0 AND [ReservedQuantity] >= 0 AND [ReservedQuantity] <= [QuantityOnHand]");
                    table.ForeignKey(
                        name: "FK_SellerInventories_BlanketModels_BlanketModelId",
                        column: x => x.BlanketModelId,
                        principalTable: "BlanketModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SellerInventories_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SellerInventories_BlanketModelId",
                table: "SellerInventories",
                column: "BlanketModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerInventories_SellerId_BlanketModelId",
                table: "SellerInventories",
                columns: new[] { "SellerId", "BlanketModelId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SellerInventories");
        }
    }
}
