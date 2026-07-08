using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyComfort.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductionCapacities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductionCapacities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlanketModelId = table.Column<int>(type: "int", nullable: false),
                    DailyCapacity = table.Column<int>(type: "int", nullable: false),
                    WeeklyCapacity = table.Column<int>(type: "int", nullable: false),
                    CurrentPendingQuantity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LeadTimeDays = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionCapacities", x => x.Id);
                    table.CheckConstraint("CK_ProductionCapacities_Values", "DailyCapacity >= 0\r\n            AND WeeklyCapacity >= 0\r\n            AND CurrentPendingQuantity >= 0\r\n            AND LeadTimeDays >= 0");
                    table.ForeignKey(
                        name: "FK_ProductionCapacities_BlanketModels_BlanketModelId",
                        column: x => x.BlanketModelId,
                        principalTable: "BlanketModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionCapacities_BlanketModelId",
                table: "ProductionCapacities",
                column: "BlanketModelId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductionCapacities");
        }
    }
}
