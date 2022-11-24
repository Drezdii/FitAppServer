using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAppServer.DataAccess.Migrations
{
    public partial class addexerciseonerepmax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExerciseId",
                table: "OneRepMaxes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OneRepMaxes_ExerciseId",
                table: "OneRepMaxes",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_OneRepMaxes_Exercises_ExerciseId",
                table: "OneRepMaxes",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OneRepMaxes_Exercises_ExerciseId",
                table: "OneRepMaxes");

            migrationBuilder.DropIndex(
                name: "IX_OneRepMaxes_ExerciseId",
                table: "OneRepMaxes");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "OneRepMaxes");
        }
    }
}
