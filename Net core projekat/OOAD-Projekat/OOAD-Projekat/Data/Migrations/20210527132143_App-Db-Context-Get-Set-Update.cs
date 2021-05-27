using Microsoft.EntityFrameworkCore.Migrations;

namespace OOAD_Projekat.Data.Migrations
{
    public partial class AppDbContextGetSetUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_AspNetUsers_UserId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_QuestionID",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_AspNetUsers_UserId",
                table: "ChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUser_Chat_ChatId",
                table: "ChatUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_AspNetUsers_UserId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Answer_AnswerId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Question_QuestionId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_TagPost_Question_QuestionId",
                table: "TagPost");

            migrationBuilder.DropForeignKey(
                name: "FK_TagPost_Tag_TagId",
                table: "TagPost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagPost",
                table: "TagPost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rating",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostType",
                table: "PostType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatUser",
                table: "ChatUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat",
                table: "Chat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.RenameTable(
                name: "TagPost",
                newName: "TagPosts");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Rating",
                newName: "Ratings");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "Questions");

            migrationBuilder.RenameTable(
                name: "PostType",
                newName: "PostTypes");

            migrationBuilder.RenameTable(
                name: "ChatUser",
                newName: "ChatUsers");

            migrationBuilder.RenameTable(
                name: "Chat",
                newName: "Chats");

            migrationBuilder.RenameTable(
                name: "Answer",
                newName: "Answers");

            migrationBuilder.RenameIndex(
                name: "IX_TagPost_QuestionId",
                table: "TagPosts",
                newName: "IX_TagPosts_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_QuestionId",
                table: "Ratings",
                newName: "IX_Ratings_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_AnswerId",
                table: "Ratings",
                newName: "IX_Ratings_AnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_UserId",
                table: "Questions",
                newName: "IX_Questions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatUser_UserId",
                table: "ChatUsers",
                newName: "IX_ChatUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_UserId",
                table: "Answers",
                newName: "IX_Answers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_QuestionID",
                table: "Answers",
                newName: "IX_Answers_QuestionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagPosts",
                table: "TagPosts",
                columns: new[] { "TagId", "QuestionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTypes",
                table: "PostTypes",
                column: "PostTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatUsers",
                table: "ChatUsers",
                columns: new[] { "ChatId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_AspNetUsers_UserId",
                table: "Answers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionID",
                table: "Answers",
                column: "QuestionID",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUsers_AspNetUsers_UserId",
                table: "ChatUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUsers_Chats_ChatId",
                table: "ChatUsers",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AspNetUsers_UserId",
                table: "Questions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Answers_AnswerId",
                table: "Ratings",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Questions_QuestionId",
                table: "Ratings",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TagPosts_Questions_QuestionId",
                table: "TagPosts",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagPosts_Tags_TagId",
                table: "TagPosts",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_AspNetUsers_UserId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionID",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsers_AspNetUsers_UserId",
                table: "ChatUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsers_Chats_ChatId",
                table: "ChatUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AspNetUsers_UserId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Answers_AnswerId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Questions_QuestionId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_TagPosts_Questions_QuestionId",
                table: "TagPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_TagPosts_Tags_TagId",
                table: "TagPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagPosts",
                table: "TagPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTypes",
                table: "PostTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatUsers",
                table: "ChatUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "TagPosts",
                newName: "TagPost");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "Rating");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Question");

            migrationBuilder.RenameTable(
                name: "PostTypes",
                newName: "PostType");

            migrationBuilder.RenameTable(
                name: "ChatUsers",
                newName: "ChatUser");

            migrationBuilder.RenameTable(
                name: "Chats",
                newName: "Chat");

            migrationBuilder.RenameTable(
                name: "Answers",
                newName: "Answer");

            migrationBuilder.RenameIndex(
                name: "IX_TagPosts_QuestionId",
                table: "TagPost",
                newName: "IX_TagPost_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_QuestionId",
                table: "Rating",
                newName: "IX_Rating_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_AnswerId",
                table: "Rating",
                newName: "IX_Rating_AnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_UserId",
                table: "Question",
                newName: "IX_Question_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatUsers_UserId",
                table: "ChatUser",
                newName: "IX_ChatUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_UserId",
                table: "Answer",
                newName: "IX_Answer_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_QuestionID",
                table: "Answer",
                newName: "IX_Answer_QuestionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagPost",
                table: "TagPost",
                columns: new[] { "TagId", "QuestionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rating",
                table: "Rating",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostType",
                table: "PostType",
                column: "PostTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatUser",
                table: "ChatUser",
                columns: new[] { "ChatId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat",
                table: "Chat",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_AspNetUsers_UserId",
                table: "Answer",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_QuestionID",
                table: "Answer",
                column: "QuestionID",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_AspNetUsers_UserId",
                table: "ChatUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUser_Chat_ChatId",
                table: "ChatUser",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_AspNetUsers_UserId",
                table: "Question",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Answer_AnswerId",
                table: "Rating",
                column: "AnswerId",
                principalTable: "Answer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Question_QuestionId",
                table: "Rating",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TagPost_Question_QuestionId",
                table: "TagPost",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagPost_Tag_TagId",
                table: "TagPost",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
