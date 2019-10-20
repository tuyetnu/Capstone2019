using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Admins_AuthorAdminAdminId",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "AuthorAdminAdminId",
                table: "News",
                newName: "AuthorAdminId");

            migrationBuilder.RenameIndex(
                name: "IX_News_AuthorAdminAdminId",
                table: "News",
                newName: "IX_News_AuthorAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Admins_AuthorAdminId",
                table: "News",
                column: "AuthorAdminId",
                principalTable: "Admins",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Admins_AuthorAdminId",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "AuthorAdminId",
                table: "News",
                newName: "AuthorAdminAdminId");

            migrationBuilder.RenameIndex(
                name: "IX_News_AuthorAdminId",
                table: "News",
                newName: "IX_News_AuthorAdminAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Admins_AuthorAdminAdminId",
                table: "News",
                column: "AuthorAdminAdminId",
                principalTable: "Admins",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
