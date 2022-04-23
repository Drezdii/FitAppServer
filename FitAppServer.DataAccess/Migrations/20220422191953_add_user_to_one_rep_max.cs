using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAppServer.DataAccess.Migrations
{
    public partial class add_user_to_one_rep_max : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "OneRepMaxes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OneRepMaxes_UserId",
                table: "OneRepMaxes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OneRepMaxes_Users_UserId",
                table: "OneRepMaxes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OneRepMaxes_Users_UserId",
                table: "OneRepMaxes");

            migrationBuilder.DropIndex(
                name: "IX_OneRepMaxes_UserId",
                table: "OneRepMaxes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OneRepMaxes");
        }
    }
}
