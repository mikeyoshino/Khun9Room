using Microsoft.EntityFrameworkCore.Migrations;

namespace Khun9Room.Data.Migrations
{
    public partial class addAppUserIdToUnitNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UnitNumbers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitNumbers_ApplicationUserId",
                table: "UnitNumbers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitNumbers_AspNetUsers_ApplicationUserId",
                table: "UnitNumbers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitNumbers_AspNetUsers_ApplicationUserId",
                table: "UnitNumbers");

            migrationBuilder.DropIndex(
                name: "IX_UnitNumbers_ApplicationUserId",
                table: "UnitNumbers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UnitNumbers");
        }
    }
}
