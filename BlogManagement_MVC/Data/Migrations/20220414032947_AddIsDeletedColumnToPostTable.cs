using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement_MVC.Data.Migrations
{
    public partial class AddIsDeletedColumnToPostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Post",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Post");
        }
    }
}
