using Microsoft.EntityFrameworkCore.Migrations;

namespace FitAppServer.DataAccess.Migrations
{
    public partial class exInfofix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_ExerciseInfo_ExerciseInfoID",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "ExerciseInfoID",
                table: "Exercises",
                newName: "ExerciseInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_ExerciseInfoID",
                table: "Exercises",
                newName: "IX_Exercises_ExerciseInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_ExerciseInfo_ExerciseInfoId",
                table: "Exercises",
                column: "ExerciseInfoId",
                principalTable: "ExerciseInfo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_ExerciseInfo_ExerciseInfoId",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "ExerciseInfoId",
                table: "Exercises",
                newName: "ExerciseInfoID");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_ExerciseInfoId",
                table: "Exercises",
                newName: "IX_Exercises_ExerciseInfoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_ExerciseInfo_ExerciseInfoID",
                table: "Exercises",
                column: "ExerciseInfoID",
                principalTable: "ExerciseInfo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
