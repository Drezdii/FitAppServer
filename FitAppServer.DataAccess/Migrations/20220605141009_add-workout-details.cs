using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FitAppServer.DataAccess.Migrations
{
    public partial class addworkoutdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseInfo_ExerciseInfo_ExerciseInfoId",
                table: "ExerciseInfo");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseInfo_ExerciseInfoId",
                table: "ExerciseInfo");

            migrationBuilder.DropColumn(
                name: "ExerciseInfoId",
                table: "ExerciseInfo");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutDetailsId",
                table: "Workouts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutProgramId",
                table: "Workouts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExerciseInfoId",
                table: "OneRepMaxes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkoutProgram",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutProgram", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutProgramDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProgramId = table.Column<int>(type: "integer", nullable: false),
                    Cycle = table.Column<int>(type: "integer", nullable: false),
                    Week = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutProgramDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutProgramDetail_WorkoutProgram_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "WorkoutProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_WorkoutDetailsId",
                table: "Workouts",
                column: "WorkoutDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_WorkoutProgramId",
                table: "Workouts",
                column: "WorkoutProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_OneRepMaxes_ExerciseInfoId",
                table: "OneRepMaxes",
                column: "ExerciseInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutProgramDetail_ProgramId",
                table: "WorkoutProgramDetail",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_OneRepMaxes_ExerciseInfo_ExerciseInfoId",
                table: "OneRepMaxes",
                column: "ExerciseInfoId",
                principalTable: "ExerciseInfo",
                principalColumn: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OneRepMaxes_ExerciseInfo_ExerciseInfoId",
                table: "OneRepMaxes");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_WorkoutProgram_WorkoutProgramId",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_WorkoutProgramDetail_WorkoutDetailsId",
                table: "Workouts");

            migrationBuilder.DropTable(
                name: "WorkoutProgramDetail");

            migrationBuilder.DropTable(
                name: "WorkoutProgram");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_WorkoutDetailsId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_WorkoutProgramId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_OneRepMaxes_ExerciseInfoId",
                table: "OneRepMaxes");

            migrationBuilder.DropColumn(
                name: "WorkoutDetailsId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "WorkoutProgramId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "ExerciseInfoId",
                table: "OneRepMaxes");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseInfoId",
                table: "ExerciseInfo",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseInfo_ExerciseInfoId",
                table: "ExerciseInfo",
                column: "ExerciseInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseInfo_ExerciseInfo_ExerciseInfoId",
                table: "ExerciseInfo",
                column: "ExerciseInfoId",
                principalTable: "ExerciseInfo",
                principalColumn: "Id");
        }
    }
}
