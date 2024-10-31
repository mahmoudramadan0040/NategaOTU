using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlOctoberTechnologyUniversitySystem.Migrations
{
    /// <inheritdoc />
    public partial class support_float_string : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<JsonElement>(
                name: "totalScore",
                table: "StudentSubjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<JsonElement>(
                name: "SemesterScore",
                table: "StudentSubjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<JsonElement>(
                name: "FinalExamScore",
                table: "StudentSubjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "totalScore",
                table: "StudentSubjects",
                type: "real",
                nullable: false,
                oldClrType: typeof(JsonElement),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<float>(
                name: "SemesterScore",
                table: "StudentSubjects",
                type: "real",
                nullable: false,
                oldClrType: typeof(JsonElement),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<float>(
                name: "FinalExamScore",
                table: "StudentSubjects",
                type: "real",
                nullable: false,
                oldClrType: typeof(JsonElement),
                oldType: "nvarchar(max)");
        }
    }
}
