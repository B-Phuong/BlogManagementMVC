using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement_MVC.Data.Migrations
{
    public partial class AddVoteAndImageColumnToPostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Post",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalVote",
                table: "Post",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PostVM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    MetaTitle = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Published = table.Column<byte>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    PublishedAt = table.Column<DateTime>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    AuthorId = table.Column<string>(nullable: true),
                    ImageURL = table.Column<string>(nullable: true),
                    TotalVote = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostVM_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserInfoVM",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    RegisteredAt = table.Column<DateTime>(nullable: false),
                    Intro = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfoVM", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostVM_AuthorId",
                table: "PostVM",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostVM");

            migrationBuilder.DropTable(
                name: "UserInfoVM");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "TotalVote",
                table: "Post");
        }
    }
}
