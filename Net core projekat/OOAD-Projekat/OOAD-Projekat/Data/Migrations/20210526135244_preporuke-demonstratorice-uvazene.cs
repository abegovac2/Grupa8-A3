using Microsoft.EntityFrameworkCore.Migrations;

namespace OOAD_Projekat.Data.Migrations
{
    public partial class preporukedemonstratoriceuvazene : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotQuestion");

            migrationBuilder.AddColumn<bool>(
                name: "HotQuestion",
                table: "Question",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotQuestion",
                table: "Question");

            migrationBuilder.CreateTable(
                name: "HotQuestion",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_HotQuestion_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotQuestion_QuestionId",
                table: "HotQuestion",
                column: "QuestionId");
        }
    }
}
