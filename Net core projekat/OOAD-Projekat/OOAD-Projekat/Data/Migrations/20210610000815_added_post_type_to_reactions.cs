using Microsoft.EntityFrameworkCore.Migrations;

namespace OOAD_Projekat.Data.Migrations
{
    public partial class added_post_type_to_reactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostTypeId",
                table: "Reactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_PostTypeId",
                table: "Reactions",
                column: "PostTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_PostTypes_PostTypeId",
                table: "Reactions",
                column: "PostTypeId",
                principalTable: "PostTypes",
                principalColumn: "PostTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_PostTypes_PostTypeId",
                table: "Reactions");

            migrationBuilder.DropIndex(
                name: "IX_Reactions_PostTypeId",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "PostTypeId",
                table: "Reactions");
        }
    }
}
