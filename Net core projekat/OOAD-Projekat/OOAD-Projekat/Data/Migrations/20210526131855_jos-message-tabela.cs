using Microsoft.EntityFrameworkCore.Migrations;

namespace OOAD_Projekat.Data.Migrations
{
    public partial class josmessagetabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_Chat_ChatId",
                table: "ChatUser");

            migrationBuilder.DropTable(
                name: "Chat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_Chat_ChatId",
                table: "ChatUser",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
