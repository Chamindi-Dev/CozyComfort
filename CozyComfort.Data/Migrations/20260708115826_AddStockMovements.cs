using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyComfort.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStockMovements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockMovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlanketModelId = table.Column<int>(type: "int", nullable: false),
                    FactoryInventoryId = table.Column<int>(type: "int", nullable: true),
                    DistributorInventoryId = table.Column<int>(type: "int", nullable: true),
                    SellerInventoryId = table.Column<int>(type: "int", nullable: true),
                    MovementType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TransferOrderId = table.Column<int>(type: "int", nullable: true),
                    CustomerOrderId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovements", x => x.Id);
                    table.CheckConstraint("CK_StockMovements_MovementType", "[MovementType] IN ('Reserve','Release','Issue','Receive','Adjust')");
                    table.CheckConstraint("CK_StockMovements_OnlyOneInventory", "(\r\n            (CASE WHEN [FactoryInventoryId] IS NOT NULL THEN 1 ELSE 0 END) +\r\n            (CASE WHEN [DistributorInventoryId] IS NOT NULL THEN 1 ELSE 0 END) +\r\n            (CASE WHEN [SellerInventoryId] IS NOT NULL THEN 1 ELSE 0 END)\r\n          ) = 1");
                    table.CheckConstraint("CK_StockMovements_Quantity", "[Quantity] > 0");
                    table.ForeignKey(
                        name: "FK_StockMovements_BlanketModels_BlanketModelId",
                        column: x => x.BlanketModelId,
                        principalTable: "BlanketModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovements_CustomerOrders_CustomerOrderId",
                        column: x => x.CustomerOrderId,
                        principalTable: "CustomerOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovements_DistributorInventories_DistributorInventoryId",
                        column: x => x.DistributorInventoryId,
                        principalTable: "DistributorInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovements_FactoryInventories_FactoryInventoryId",
                        column: x => x.FactoryInventoryId,
                        principalTable: "FactoryInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovements_SellerInventories_SellerInventoryId",
                        column: x => x.SellerInventoryId,
                        principalTable: "SellerInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovements_TransferOrders_TransferOrderId",
                        column: x => x.TransferOrderId,
                        principalTable: "TransferOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_BlanketModelId",
                table: "StockMovements",
                column: "BlanketModelId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_CustomerOrderId",
                table: "StockMovements",
                column: "CustomerOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_DistributorInventoryId",
                table: "StockMovements",
                column: "DistributorInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_FactoryInventoryId",
                table: "StockMovements",
                column: "FactoryInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_SellerInventoryId",
                table: "StockMovements",
                column: "SellerInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_TransferOrderId",
                table: "StockMovements",
                column: "TransferOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockMovements");
        }
    }
}
