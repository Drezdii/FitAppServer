using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAppServer.DataAccess.Migrations
{
    public partial class update_challenges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "ChallengeEntries");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Challenges",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "ChallengeEntries",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "ChallengeEntries");

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "ChallengeEntries",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
