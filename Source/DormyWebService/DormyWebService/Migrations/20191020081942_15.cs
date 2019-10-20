using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Users_StudentId",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Admins_AuthorAdminStudentId",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "AuthorAdminStudentId",
                table: "News",
                newName: "AuthorAdminAdminId");

            migrationBuilder.RenameIndex(
                name: "IX_News_AuthorAdminStudentId",
                table: "News",
                newName: "IX_News_AuthorAdminAdminId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Admins",
                newName: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Users_AdminId",
                table: "Admins",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Admins_AuthorAdminAdminId",
                table: "News",
                column: "AuthorAdminAdminId",
                principalTable: "Admins",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Users_AdminId",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Admins_AuthorAdminAdminId",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "AuthorAdminAdminId",
                table: "News",
                newName: "AuthorAdminStudentId");

            migrationBuilder.RenameIndex(
                name: "IX_News_AuthorAdminAdminId",
                table: "News",
                newName: "IX_News_AuthorAdminStudentId");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "Admins",
                newName: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Users_StudentId",
                table: "Admins",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Admins_AuthorAdminStudentId",
                table: "News",
                column: "AuthorAdminStudentId",
                principalTable: "Admins",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
