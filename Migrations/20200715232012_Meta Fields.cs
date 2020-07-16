using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Migrations
{
    public partial class MetaFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "posts");
        }
    }
}
