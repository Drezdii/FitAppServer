using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAppServer.DataAccess.Migrations
{
    public partial class change_one_rep_max : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OneRepMaxes_Workouts_WorkoutId",
                table: "OneRepMaxes");

            migrationBuilder.RenameColumn(
                name: "WorkoutId",
                table: "OneRepMaxes",
                newName: "SetId");

            migrationBuilder.RenameIndex(
                name: "IX_OneRepMaxes_WorkoutId",
                table: "OneRepMaxes",
                newName: "IX_OneRepMaxes_SetId");

            migrationBuilder.AddForeignKey(
                name: "FK_OneRepMaxes_Sets_SetId",
                table: "OneRepMaxes",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OneRepMaxes_Sets_SetId",
                table: "OneRepMaxes");

            migrationBuilder.RenameColumn(
                name: "SetId",
                table: "OneRepMaxes",
                newName: "WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_OneRepMaxes_SetId",
                table: "OneRepMaxes",
                newName: "IX_OneRepMaxes_WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_OneRepMaxes_Workouts_WorkoutId",
                table: "OneRepMaxes",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
