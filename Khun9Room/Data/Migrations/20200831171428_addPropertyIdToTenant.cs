using Microsoft.EntityFrameworkCore.Migrations;

namespace Khun9Room.Data.Migrations
{
    public partial class addPropertyIdToTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Tenants",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Tenants");
        }
    }
}
