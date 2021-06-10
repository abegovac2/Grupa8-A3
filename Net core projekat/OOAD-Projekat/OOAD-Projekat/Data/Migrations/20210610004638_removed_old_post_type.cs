using Microsoft.EntityFrameworkCore.Migrations;

namespace OOAD_Projekat.Data.Migrations
{
    public partial class removed_old_post_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_PostTypes_PostTypeId",
                table: "Reactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTypes",
                table: "PostTypes");

            migrationBuilder.RenameTable(
                name: "PostTypes",
                newName: "PostType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostType",
                table: "PostType",
                column: "PostTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_PostType_PostTypeId",
                table: "Reactions",
                column: "PostTypeId",
                principalTable: "PostType",
                principalColumn: "PostTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_PostType_PostTypeId",
                table: "Reactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostType",
                table: "PostType");

            migrationBuilder.RenameTable(
                name: "PostType",
                newName: "PostTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTypes",
                table: "PostTypes",
                column: "PostTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_PostTypes_PostTypeId",
                table: "Reactions",
                column: "PostTypeId",
                principalTable: "PostTypes",
                principalColumn: "PostTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
