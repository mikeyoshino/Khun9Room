using Microsoft.EntityFrameworkCore.Migrations;

namespace Khun9Room.Data.Migrations
{
    public partial class addUnitNumberToRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitNumberId",
                table: "Rooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UnitNumbers",
                columns: table => new
                {
                    UnitNumberId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitPrefix = table.Column<string>(nullable: true),
                    IsTaken = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitNumbers", x => x.UnitNumberId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_UnitNumberId",
                table: "Rooms",
                column: "UnitNumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_UnitNumbers_UnitNumberId",
                table: "Rooms",
                column: "UnitNumberId",
                principalTable: "UnitNumbers",
                principalColumn: "UnitNumberId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_UnitNumbers_UnitNumberId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "UnitNumbers");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_UnitNumberId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "UnitNumberId",
                table: "Rooms");
        }
    }
}
