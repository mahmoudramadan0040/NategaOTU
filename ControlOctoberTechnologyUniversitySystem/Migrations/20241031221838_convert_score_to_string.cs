using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlOctoberTechnologyUniversitySystem.Migrations
{
    /// <inheritdoc />
    public partial class convert_score_to_string : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "totalScore",
                table: "StudentSubjects",
                newName: "TotalScoreJson");

            migrationBuilder.RenameColumn(
                name: "SemesterScore",
                table: "StudentSubjects",
                newName: "SemesterScoreJson");

            migrationBuilder.RenameColumn(
                name: "FinalExamScore",
                table: "StudentSubjects",
                newName: "FinalExamScoreJson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalScoreJson",
                table: "StudentSubjects",
                newName: "totalScore");

            migrationBuilder.RenameColumn(
                name: "SemesterScoreJson",
                table: "StudentSubjects",
                newName: "SemesterScore");

            migrationBuilder.RenameColumn(
                name: "FinalExamScoreJson",
                table: "StudentSubjects",
                newName: "FinalExamScore");
        }
    }
}
