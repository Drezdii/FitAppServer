using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FitAppServer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatechallengeids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChallengeEntries",
                table: "ChallengeEntries");

            migrationBuilder.DropIndex(
                name: "IX_ChallengeEntries_UserId",
                table: "ChallengeEntries");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ChallengeEntries");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseInfoId",
                table: "OneRepMaxes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Challenges",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "ChallengeId",
                table: "ChallengeEntries",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChallengeEntries",
                table: "ChallengeEntries",
                columns: new[] { "UserId", "ChallengeId" });

            migrationBuilder.CreateIndex(
                name: "IX_OneRepMaxes_ExerciseInfoId",
                table: "OneRepMaxes",
                column: "ExerciseInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OneRepMaxes_ExerciseInfo_ExerciseInfoId",
                table: "OneRepMaxes",
                column: "ExerciseInfoId",
                principalTable: "ExerciseInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OneRepMaxes_ExerciseInfo_ExerciseInfoId",
                table: "OneRepMaxes");

            migrationBuilder.DropIndex(
                name: "IX_OneRepMaxes_ExerciseInfoId",
                table: "OneRepMaxes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChallengeEntries",
                table: "ChallengeEntries");

            migrationBuilder.DropColumn(
                name: "ExerciseInfoId",
                table: "OneRepMaxes");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Challenges",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ChallengeId",
                table: "ChallengeEntries",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ChallengeEntries",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChallengeEntries",
                table: "ChallengeEntries",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeEntries_UserId",
                table: "ChallengeEntries",
                column: "UserId");
        }
    }
}
