using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyComfort.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDistributorInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributorInventories_BlanketModels_BlanketModelId",
                table: "DistributorInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_DistributorInventories_Distributors_DistributorId",
                table: "DistributorInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_FactoryInventories_BlanketModels_BlanketModelId",
                table: "FactoryInventories");

            migrationBuilder.DropIndex(
                name: "IX_FactoryInventories_BlanketModelId",
                table: "FactoryInventories");

            migrationBuilder.DropIndex(
                name: "IX_DistributorInventories_DistributorId",
                table: "DistributorInventories");

            migrationBuilder.AlterColumn<int>(
                name: "ReservedQuantity",
                table: "FactoryInventories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "QuantityOnHand",
                table: "FactoryInventories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "FactoryInventories",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "ReservedQuantity",
                table: "DistributorInventories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "QuantityOnHand",
                table: "DistributorInventories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "DistributorInventories",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "AvailableQuantity",
                table: "FactoryInventories",
                type: "int",
                nullable: false,
                computedColumnSql: "[QuantityOnHand]-[ReservedQuantity]",
                stored: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AvailableQuantity",
                table: "DistributorInventories",
                type: "int",
                nullable: false,
                computedColumnSql: "[QuantityOnHand]-[ReservedQuantity]",
                stored: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_FactoryInventories_BlanketModelId",
                table: "FactoryInventories",
                column: "BlanketModelId",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_FactoryInventories_Qty",
                table: "FactoryInventories",
                sql: "[QuantityOnHand] >= 0 AND [ReservedQuantity] >= 0 AND [ReservedQuantity] <= [QuantityOnHand]");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorInventories_DistributorId_BlanketModelId",
                table: "DistributorInventories",
                columns: new[] { "DistributorId", "BlanketModelId" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_DistributorInventories_Qty",
                table: "DistributorInventories",
                sql: "[QuantityOnHand] >= 0 AND [ReservedQuantity] >= 0 AND [ReservedQuantity] <= [QuantityOnHand]");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributorInventories_BlanketModels_BlanketModelId",
                table: "DistributorInventories",
                column: "BlanketModelId",
                principalTable: "BlanketModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DistributorInventories_Distributors_DistributorId",
                table: "DistributorInventories",
                column: "DistributorId",
                principalTable: "Distributors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FactoryInventories_BlanketModels_BlanketModelId",
                table: "FactoryInventories",
                column: "BlanketModelId",
                principalTable: "BlanketModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributorInventories_BlanketModels_BlanketModelId",
                table: "DistributorInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_DistributorInventories_Distributors_DistributorId",
                table: "DistributorInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_FactoryInventories_BlanketModels_BlanketModelId",
                table: "FactoryInventories");

            migrationBuilder.DropIndex(
                name: "IX_FactoryInventories_BlanketModelId",
                table: "FactoryInventories");

            migrationBuilder.DropCheckConstraint(
                name: "CK_FactoryInventories_Qty",
                table: "FactoryInventories");

            migrationBuilder.DropIndex(
                name: "IX_DistributorInventories_DistributorId_BlanketModelId",
                table: "DistributorInventories");

            migrationBuilder.DropCheckConstraint(
                name: "CK_DistributorInventories_Qty",
                table: "DistributorInventories");

            migrationBuilder.AlterColumn<int>(
                name: "ReservedQuantity",
                table: "FactoryInventories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "QuantityOnHand",
                table: "FactoryInventories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "FactoryInventories",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<int>(
                name: "AvailableQuantity",
                table: "FactoryInventories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "[QuantityOnHand]-[ReservedQuantity]");

            migrationBuilder.AlterColumn<int>(
                name: "ReservedQuantity",
                table: "DistributorInventories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "QuantityOnHand",
                table: "DistributorInventories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "DistributorInventories",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<int>(
                name: "AvailableQuantity",
                table: "DistributorInventories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "[QuantityOnHand]-[ReservedQuantity]");

            migrationBuilder.CreateIndex(
                name: "IX_FactoryInventories_BlanketModelId",
                table: "FactoryInventories",
                column: "BlanketModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorInventories_DistributorId",
                table: "DistributorInventories",
                column: "DistributorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributorInventories_BlanketModels_BlanketModelId",
                table: "DistributorInventories",
                column: "BlanketModelId",
                principalTable: "BlanketModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DistributorInventories_Distributors_DistributorId",
                table: "DistributorInventories",
                column: "DistributorId",
                principalTable: "Distributors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FactoryInventories_BlanketModels_BlanketModelId",
                table: "FactoryInventories",
                column: "BlanketModelId",
                principalTable: "BlanketModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
