using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAppServer.DataAccess.Migrations
{
    public partial class adddetailscolumnrename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_WorkoutProgramDetails_WorkoutDetailsId",
                table: "Workouts");

            migrationBuilder.RenameColumn(
                name: "WorkoutDetailsId",
                table: "Workouts",
                newName: "WorkoutProgramDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_Workouts_WorkoutDetailsId",
                table: "Workouts",
                newName: "IX_Workouts_WorkoutProgramDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_WorkoutProgramDetails_WorkoutProgramDetailsId",
                table: "Workouts",
                column: "WorkoutProgramDetailsId",
                principalTable: "WorkoutProgramDetails",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_WorkoutProgramDetails_WorkoutProgramDetailsId",
                table: "Workouts");

            migrationBuilder.RenameColumn(
                name: "WorkoutProgramDetailsId",
                table: "Workouts",
                newName: "WorkoutDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_Workouts_WorkoutProgramDetailsId",
                table: "Workouts",
                newName: "IX_Workouts_WorkoutDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_WorkoutProgramDetails_WorkoutDetailsId",
                table: "Workouts",
                column: "WorkoutDetailsId",
                principalTable: "WorkoutProgramDetails",
                principalColumn: "Id");
        }
    }
}
