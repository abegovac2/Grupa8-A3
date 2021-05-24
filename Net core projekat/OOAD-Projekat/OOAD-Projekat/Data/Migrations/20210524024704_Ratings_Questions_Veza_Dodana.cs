using Microsoft.EntityFrameworkCore.Migrations;

namespace OOAD_Projekat.Data.Migrations
{
    public partial class Ratings_Questions_Veza_Dodana : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Rating",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rating_QuestionId",
                table: "Rating",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Question_QuestionId",
                table: "Rating",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Question_QuestionId",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_QuestionId",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Rating");
        }
    }
}
