using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAppServer.DataAccess.Migrations
{
    public partial class change_onerepmax2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OneRepMaxes_ExerciseInfo_ExerciseInfoId",
                table: "OneRepMaxes");

            migrationBuilder.DropIndex(
                name: "IX_OneRepMaxes_ExerciseInfoId",
                table: "OneRepMaxes");

            migrationBuilder.DropColumn(
                name: "ExerciseInfoId",
                table: "OneRepMaxes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExerciseInfoId",
                table: "OneRepMaxes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OneRepMaxes_ExerciseInfoId",
                table: "OneRepMaxes",
                column: "ExerciseInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OneRepMaxes_ExerciseInfo_ExerciseInfoId",
                table: "OneRepMaxes",
                column: "ExerciseInfoId",
                principalTable: "ExerciseInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
