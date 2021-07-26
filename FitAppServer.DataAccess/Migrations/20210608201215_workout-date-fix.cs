using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitAppServer.DataAccess.Migrations
{
    public partial class workoutdatefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Workouts",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Workouts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Workouts");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Workouts",
                newName: "Date");
        }
    }
}
