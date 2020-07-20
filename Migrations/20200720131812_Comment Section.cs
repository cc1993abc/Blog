using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Migrations
{
    public partial class CommentSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mainComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    PostId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mainComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mainComments_posts_PostId",
                        column: x => x.PostId,
                        principalTable: "posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "subComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    MainCommentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subComments_mainComments_MainCommentId",
                        column: x => x.MainCommentId,
                        principalTable: "mainComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mainComments_PostId",
                table: "mainComments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_subComments_MainCommentId",
                table: "subComments",
                column: "MainCommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "subComments");

            migrationBuilder.DropTable(
                name: "mainComments");
        }
    }
}
