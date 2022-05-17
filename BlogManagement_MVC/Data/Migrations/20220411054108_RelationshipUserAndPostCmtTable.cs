using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement_MVC.Data.Migrations
{
    public partial class RelationshipUserAndPostCmtTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorCommentId",
                table: "PostComments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_AuthorCommentId",
                table: "PostComments",
                column: "AuthorCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_Users_AuthorCommentId",
                table: "PostComments",
                column: "AuthorCommentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_Users_AuthorCommentId",
                table: "PostComments");

            migrationBuilder.DropIndex(
                name: "IX_PostComments_AuthorCommentId",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "AuthorCommentId",
                table: "PostComments");
        }
    }
}
