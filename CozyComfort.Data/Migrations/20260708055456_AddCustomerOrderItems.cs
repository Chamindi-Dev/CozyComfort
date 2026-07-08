using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyComfort.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerOrderItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerOrderId = table.Column<int>(type: "int", nullable: false),
                    BlanketModelId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, computedColumnSql: "[Quantity] * [UnitPrice]", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrderItems", x => x.Id);
                    table.CheckConstraint("CK_CustomerOrderItems_Values", "[Quantity] > 0 AND [UnitPrice] >= 0");
                    table.ForeignKey(
                        name: "FK_CustomerOrderItems_BlanketModels_BlanketModelId",
                        column: x => x.BlanketModelId,
                        principalTable: "BlanketModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerOrderItems_CustomerOrders_CustomerOrderId",
                        column: x => x.CustomerOrderId,
                        principalTable: "CustomerOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderItems_BlanketModelId",
                table: "CustomerOrderItems",
                column: "BlanketModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderItems_CustomerOrderId",
                table: "CustomerOrderItems",
                column: "CustomerOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerOrderItems");
        }
    }
}
