using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAppServer.DataAccess.Migrations
{
    public partial class fixnames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_ExerciseInfo_ExerciseInfoID",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Workouts_WorkoutID",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Exercises_ExerciseID",
                table: "Sets");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_UserID",
                table: "Workouts");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Workouts",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Workouts",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Workouts_UserID",
                table: "Workouts",
                newName: "IX_Workouts_UserId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ExerciseID",
                table: "Sets",
                newName: "ExerciseId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Sets",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Sets_ExerciseID",
                table: "Sets",
                newName: "IX_Sets_ExerciseId");

            migrationBuilder.RenameColumn(
                name: "WorkoutID",
                table: "Exercises",
                newName: "WorkoutId");

            migrationBuilder.RenameColumn(
                name: "ExerciseInfoID",
                table: "Exercises",
                newName: "ExerciseInfoId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Exercises",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_WorkoutID",
                table: "Exercises",
                newName: "IX_Exercises_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_ExerciseInfoID",
                table: "Exercises",
                newName: "IX_Exercises_ExerciseInfoId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ExerciseInfo",
                newName: "Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Workouts",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Workouts",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Workouts",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExerciseInfo",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_ExerciseInfo_ExerciseInfoId",
                table: "Exercises",
                column: "ExerciseInfoId",
                principalTable: "ExerciseInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Exercises_ExerciseId",
                table: "Sets",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_UserId",
                table: "Workouts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_ExerciseInfo_ExerciseInfoId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Workouts_WorkoutId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Exercises_ExerciseId",
                table: "Sets");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_UserId",
                table: "Workouts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Workouts",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Workouts",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts",
                newName: "IX_Workouts_UserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "Sets",
                newName: "ExerciseID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Sets",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Sets_ExerciseId",
                table: "Sets",
                newName: "IX_Sets_ExerciseID");

            migrationBuilder.RenameColumn(
                name: "WorkoutId",
                table: "Exercises",
                newName: "WorkoutID");

            migrationBuilder.RenameColumn(
                name: "ExerciseInfoId",
                table: "Exercises",
                newName: "ExerciseInfoID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Exercises",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_WorkoutId",
                table: "Exercises",
                newName: "IX_Exercises_WorkoutID");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_ExerciseInfoId",
                table: "Exercises",
                newName: "IX_Exercises_ExerciseInfoID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ExerciseInfo",
                newName: "ID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Workouts",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Workouts",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Workouts",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExerciseInfo",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_ExerciseInfo_ExerciseInfoID",
                table: "Exercises",
                column: "ExerciseInfoID",
                principalTable: "ExerciseInfo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Workouts_WorkoutID",
                table: "Exercises",
                column: "WorkoutID",
                principalTable: "Workouts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Exercises_ExerciseID",
                table: "Sets",
                column: "ExerciseID",
                principalTable: "Exercises",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_UserID",
                table: "Workouts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
