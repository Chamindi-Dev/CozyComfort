using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyComfort.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAvailabilityRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvailabilityRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerOrderId = table.Column<int>(type: "int", nullable: false),
                    BlanketModelId = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    DistributorId = table.Column<int>(type: "int", nullable: false),
                    RequestedQuantity = table.Column<int>(type: "int", nullable: false),
                    RequestLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Pending"),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpectedLeadTimeDays = table.Column<int>(type: "int", nullable: true),
                    ResponseMessage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilityRequests", x => x.Id);
                    table.CheckConstraint("CK_AvailabilityRequests_Level", "[RequestLevel] IN ('SellerToDistributor','DistributorToFactory')");
                    table.CheckConstraint("CK_AvailabilityRequests_Qty", "[RequestedQuantity] > 0");
                    table.CheckConstraint("CK_AvailabilityRequests_Status", "[Status] IN ('Pending','Available','NotAvailable','ProductionPossible','Rejected')");
                    table.ForeignKey(
                        name: "FK_AvailabilityRequests_BlanketModels_BlanketModelId",
                        column: x => x.BlanketModelId,
                        principalTable: "BlanketModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AvailabilityRequests_CustomerOrders_CustomerOrderId",
                        column: x => x.CustomerOrderId,
                        principalTable: "CustomerOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AvailabilityRequests_Distributors_DistributorId",
                        column: x => x.DistributorId,
                        principalTable: "Distributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AvailabilityRequests_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilityRequests_BlanketModelId",
                table: "AvailabilityRequests",
                column: "BlanketModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilityRequests_CustomerOrderId",
                table: "AvailabilityRequests",
                column: "CustomerOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilityRequests_DistributorId",
                table: "AvailabilityRequests",
                column: "DistributorId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilityRequests_RequestNumber",
                table: "AvailabilityRequests",
                column: "RequestNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilityRequests_SellerId",
                table: "AvailabilityRequests",
                column: "SellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvailabilityRequests");
        }
    }
}
