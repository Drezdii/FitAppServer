using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAppServer.DataAccess.Migrations
{
    public partial class addworkoutdetailsfix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Programs_WorkoutProgramId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_WorkoutProgramId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "WorkoutProgramId",
                table: "Workouts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkoutProgramId",
                table: "Workouts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_WorkoutProgramId",
                table: "Workouts",
                column: "WorkoutProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Programs_WorkoutProgramId",
                table: "Workouts",
                column: "WorkoutProgramId",
                principalTable: "Programs",
                principalColumn: "Id");
        }
    }
}
