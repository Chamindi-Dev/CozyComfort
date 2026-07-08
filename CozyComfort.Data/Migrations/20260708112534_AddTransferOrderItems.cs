using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyComfort.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTransferOrderItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferOrderId = table.Column<int>(type: "int", nullable: false),
                    BlanketModelId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferOrderItems", x => x.Id);
                    table.CheckConstraint("CK_TransferOrderItems_Quantity", "[Quantity] > 0");
                    table.ForeignKey(
                        name: "FK_TransferOrderItems_BlanketModels_BlanketModelId",
                        column: x => x.BlanketModelId,
                        principalTable: "BlanketModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOrderItems_TransferOrders_TransferOrderId",
                        column: x => x.TransferOrderId,
                        principalTable: "TransferOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrderItems_BlanketModelId",
                table: "TransferOrderItems",
                column: "BlanketModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrderItems_TransferOrderId",
                table: "TransferOrderItems",
                column: "TransferOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferOrderItems");
        }
    }
}
