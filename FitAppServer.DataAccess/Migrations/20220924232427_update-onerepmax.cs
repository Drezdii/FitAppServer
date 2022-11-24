using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAppServer.DataAccess.Migrations
{
    public partial class updateonerepmax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OneRepMaxes_Exercises_ExerciseId",
                table: "OneRepMaxes");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "OneRepMaxes",
                newName: "ExerciseInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_OneRepMaxes_ExerciseId",
                table: "OneRepMaxes",
                newName: "IX_OneRepMaxes_ExerciseInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OneRepMaxes_ExerciseInfo_ExerciseInfoId",
                table: "OneRepMaxes",
                column: "ExerciseInfoId",
                principalTable: "ExerciseInfo",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OneRepMaxes_ExerciseInfo_ExerciseInfoId",
                table: "OneRepMaxes");

            migrationBuilder.RenameColumn(
                name: "ExerciseInfoId",
                table: "OneRepMaxes",
                newName: "ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_OneRepMaxes_ExerciseInfoId",
                table: "OneRepMaxes",
                newName: "IX_OneRepMaxes_ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_OneRepMaxes_Exercises_ExerciseId",
                table: "OneRepMaxes",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id");
        }
    }
}
