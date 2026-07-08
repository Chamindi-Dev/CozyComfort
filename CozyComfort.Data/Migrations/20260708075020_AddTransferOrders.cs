using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyComfort.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTransferOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerOrderId = table.Column<int>(type: "int", nullable: true),
                    FromLocationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ToLocationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FromDistributorId = table.Column<int>(type: "int", nullable: true),
                    ToDistributorId = table.Column<int>(type: "int", nullable: true),
                    FromSellerId = table.Column<int>(type: "int", nullable: true),
                    ToSellerId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Pending"),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferOrders", x => x.Id);
                    table.CheckConstraint("CK_TransferOrders_FromLocationType", "[FromLocationType] IN ('Factory','Distributor','Seller')");
                    table.CheckConstraint("CK_TransferOrders_Status", "[Status] IN ('Pending','Approved','InTransit','Completed','Cancelled')");
                    table.CheckConstraint("CK_TransferOrders_ToLocationType", "[ToLocationType] IN ('Distributor','Seller','Customer')");
                    table.ForeignKey(
                        name: "FK_TransferOrders_CustomerOrders_CustomerOrderId",
                        column: x => x.CustomerOrderId,
                        principalTable: "CustomerOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOrders_Distributors_FromDistributorId",
                        column: x => x.FromDistributorId,
                        principalTable: "Distributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOrders_Distributors_ToDistributorId",
                        column: x => x.ToDistributorId,
                        principalTable: "Distributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOrders_Sellers_FromSellerId",
                        column: x => x.FromSellerId,
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOrders_Sellers_ToSellerId",
                        column: x => x.ToSellerId,
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrders_CustomerOrderId",
                table: "TransferOrders",
                column: "CustomerOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrders_FromDistributorId",
                table: "TransferOrders",
                column: "FromDistributorId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrders_FromSellerId",
                table: "TransferOrders",
                column: "FromSellerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrders_ToDistributorId",
                table: "TransferOrders",
                column: "ToDistributorId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrders_ToSellerId",
                table: "TransferOrders",
                column: "ToSellerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrders_TransferNumber",
                table: "TransferOrders",
                column: "TransferNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferOrders");
        }
    }
}
