using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeHub.Migrations
{
    /// <inheritdoc />
    public partial class QuestionsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TeacherId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Subject = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EditedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    EditedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExamId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    AnswerA = table.Column<string>(type: "TEXT", nullable: false),
                    AnswerB = table.Column<string>(type: "TEXT", nullable: false),
                    AnswerC = table.Column<string>(type: "TEXT", nullable: true),
                    AnswerD = table.Column<string>(type: "TEXT", nullable: true),
                    AnswerE = table.Column<string>(type: "TEXT", nullable: true),
                    CorrectAnswer = table.Column<int>(type: "INTEGER", nullable: false),
                    IndividualValue = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "Question");
        }
    }
}
