using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAppServer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class bwtoworkoutlink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BodyWeightEntryId",
                table: "Workouts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_BodyWeightEntryId",
                table: "Workouts",
                column: "BodyWeightEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_BodyWeightEntries_BodyWeightEntryId",
                table: "Workouts",
                column: "BodyWeightEntryId",
                principalTable: "BodyWeightEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_BodyWeightEntries_BodyWeightEntryId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_BodyWeightEntryId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "BodyWeightEntryId",
                table: "Workouts");
        }
    }
}
