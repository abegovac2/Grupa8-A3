using Microsoft.EntityFrameworkCore.Migrations;

namespace OOAD_Projekat.Data.Migrations
{
    public partial class added_new_post_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_PostType_PostTypeId",
                table: "Reactions");

            migrationBuilder.DropTable(
                name: "PostType");

            migrationBuilder.DropIndex(
                name: "IX_Reactions_PostTypeId",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "PostTypeId",
                table: "Reactions");

            migrationBuilder.AddColumn<int>(
                name: "PostType",
                table: "Reactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostType",
                table: "Reactions");

            migrationBuilder.AddColumn<int>(
                name: "PostTypeId",
                table: "Reactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostType",
                columns: table => new
                {
                    PostTypeId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostType", x => x.PostTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_PostTypeId",
                table: "Reactions",
                column: "PostTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_PostType_PostTypeId",
                table: "Reactions",
                column: "PostTypeId",
                principalTable: "PostType",
                principalColumn: "PostTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
