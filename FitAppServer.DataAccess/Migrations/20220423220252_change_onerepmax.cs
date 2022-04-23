using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAppServer.DataAccess.Migrations
{
    public partial class change_onerepmax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lift",
                table: "OneRepMaxes",
                newName: "ExerciseInfoId");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseInfoId",
                table: "ExerciseInfo",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OneRepMaxes_ExerciseInfoId",
                table: "OneRepMaxes",
                column: "ExerciseInfoId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_OneRepMaxes_ExerciseInfo_ExerciseInfoId",
                table: "OneRepMaxes",
                column: "ExerciseInfoId",
                principalTable: "ExerciseInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseInfo_ExerciseInfo_ExerciseInfoId",
                table: "ExerciseInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_OneRepMaxes_ExerciseInfo_ExerciseInfoId",
                table: "OneRepMaxes");

            migrationBuilder.DropIndex(
                name: "IX_OneRepMaxes_ExerciseInfoId",
                table: "OneRepMaxes");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseInfo_ExerciseInfoId",
                table: "ExerciseInfo");

            migrationBuilder.DropColumn(
                name: "ExerciseInfoId",
                table: "ExerciseInfo");

            migrationBuilder.RenameColumn(
                name: "ExerciseInfoId",
                table: "OneRepMaxes",
                newName: "Lift");
        }
    }
}
