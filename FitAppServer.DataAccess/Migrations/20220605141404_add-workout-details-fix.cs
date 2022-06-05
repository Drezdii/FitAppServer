using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAppServer.DataAccess.Migrations
{
    public partial class addworkoutdetailsfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OneRepMaxes_ExerciseInfo_ExerciseInfoId",
                table: "OneRepMaxes");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutProgramDetail_WorkoutProgram_ProgramId",
                table: "WorkoutProgramDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_WorkoutProgram_WorkoutProgramId",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_WorkoutProgramDetail_WorkoutDetailsId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_OneRepMaxes_ExerciseInfoId",
                table: "OneRepMaxes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutProgramDetail",
                table: "WorkoutProgramDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutProgram",
                table: "WorkoutProgram");

            migrationBuilder.DropColumn(
                name: "ExerciseInfoId",
                table: "OneRepMaxes");

            migrationBuilder.RenameTable(
                name: "WorkoutProgramDetail",
                newName: "WorkoutProgramDetails");

            migrationBuilder.RenameTable(
                name: "WorkoutProgram",
                newName: "Programs");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutProgramDetail_ProgramId",
                table: "WorkoutProgramDetails",
                newName: "IX_WorkoutProgramDetails_ProgramId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutProgramDetails",
                table: "WorkoutProgramDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Programs",
                table: "Programs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutProgramDetails_Programs_ProgramId",
                table: "WorkoutProgramDetails",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Programs_WorkoutProgramId",
                table: "Workouts",
                column: "WorkoutProgramId",
                principalTable: "Programs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_WorkoutProgramDetails_WorkoutDetailsId",
                table: "Workouts",
                column: "WorkoutDetailsId",
                principalTable: "WorkoutProgramDetails",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutProgramDetails_Programs_ProgramId",
                table: "WorkoutProgramDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Programs_WorkoutProgramId",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_WorkoutProgramDetails_WorkoutDetailsId",
                table: "Workouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutProgramDetails",
                table: "WorkoutProgramDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Programs",
                table: "Programs");

            migrationBuilder.RenameTable(
                name: "WorkoutProgramDetails",
                newName: "WorkoutProgramDetail");

            migrationBuilder.RenameTable(
                name: "Programs",
                newName: "WorkoutProgram");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutProgramDetails_ProgramId",
                table: "WorkoutProgramDetail",
                newName: "IX_WorkoutProgramDetail_ProgramId");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseInfoId",
                table: "OneRepMaxes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutProgramDetail",
                table: "WorkoutProgramDetail",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutProgram",
                table: "WorkoutProgram",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OneRepMaxes_ExerciseInfoId",
                table: "OneRepMaxes",
                column: "ExerciseInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OneRepMaxes_ExerciseInfo_ExerciseInfoId",
                table: "OneRepMaxes",
                column: "ExerciseInfoId",
                principalTable: "ExerciseInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutProgramDetail_WorkoutProgram_ProgramId",
                table: "WorkoutProgramDetail",
                column: "ProgramId",
                principalTable: "WorkoutProgram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_WorkoutProgram_WorkoutProgramId",
                table: "Workouts",
                column: "WorkoutProgramId",
                principalTable: "WorkoutProgram",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_WorkoutProgramDetail_WorkoutDetailsId",
                table: "Workouts",
                column: "WorkoutDetailsId",
                principalTable: "WorkoutProgramDetail",
                principalColumn: "Id");
        }
    }
}
