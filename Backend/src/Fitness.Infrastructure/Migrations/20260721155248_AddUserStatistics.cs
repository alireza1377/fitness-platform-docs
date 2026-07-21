using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkoutMinutes = table.Column<int>(type: "integer", nullable: false),
                    CompletedPrograms = table.Column<int>(type: "integer", nullable: false),
                    CompletedVideos = table.Column<int>(type: "integer", nullable: false),
                    CurrentStreak = table.Column<int>(type: "integer", nullable: false),
                    BestStreak = table.Column<int>(type: "integer", nullable: false),
                    LastWorkoutDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStatistics_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProgramProgresses_FitnessProgramId",
                table: "UserProgramProgresses",
                column: "FitnessProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgramProgresses_UserId_FitnessProgramId",
                table: "UserProgramProgresses",
                columns: new[] { "UserId", "FitnessProgramId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_UserId",
                table: "UserStatistics",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProgramProgresses_FitnessPrograms_FitnessProgramId",
                table: "UserProgramProgresses",
                column: "FitnessProgramId",
                principalTable: "FitnessPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProgramProgresses_Users_UserId",
                table: "UserProgramProgresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProgramProgresses_FitnessPrograms_FitnessProgramId",
                table: "UserProgramProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProgramProgresses_Users_UserId",
                table: "UserProgramProgresses");

            migrationBuilder.DropTable(
                name: "UserStatistics");

            migrationBuilder.DropIndex(
                name: "IX_UserProgramProgresses_FitnessProgramId",
                table: "UserProgramProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserProgramProgresses_UserId_FitnessProgramId",
                table: "UserProgramProgresses");
        }
    }
}
