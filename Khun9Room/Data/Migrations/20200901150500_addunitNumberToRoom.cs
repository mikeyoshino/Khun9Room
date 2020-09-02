using Microsoft.EntityFrameworkCore.Migrations;

namespace Khun9Room.Data.Migrations
{
    public partial class addunitNumberToRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnitNumber",
                table: "Rooms",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitNumber",
                table: "Rooms");
        }
    }
}
