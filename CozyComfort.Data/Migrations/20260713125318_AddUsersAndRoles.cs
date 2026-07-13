using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyComfort.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_CustomerOrders_Status",
                table: "CustomerOrders");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_CustomerOrders_Status",
                table: "CustomerOrders",
                sql: "Status IN\r\n            (\r\n            'Pending',\r\n            'CheckingSellerStock',\r\n            'CheckingDistributorStock',\r\n            'CheckingFactoryStock',\r\n            'WaitingProduction',\r\n            'Confirmed',\r\n            'InFulfillment',\r\n            'Dispatched',\r\n            'Delivered',\r\n            'Cancelled'\r\n            )");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleName",
                table: "Roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropCheckConstraint(
                name: "CK_CustomerOrders_Status",
                table: "CustomerOrders");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CustomerOrders_Status",
                table: "CustomerOrders",
                sql: "Status IN \r\n            (\r\n            'Pending',\r\n            'CheckingSellerStock',\r\n            'CheckingDistributorStock',\r\n            'CheckingFactoryStock',\r\n            'WaitingProduction',\r\n            'Confirmed',\r\n            'InFulfillment',\r\n            'Dispatched',\r\n            'Delivered',\r\n            'Cancelled'\r\n            )");
        }
    }
}
