using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CancelContractForms",
                columns: table => new
                {
                    CancelContractFormId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    CancelationDate = table.Column<DateTime>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    StaffId = table.Column<int>(nullable: true),
                    Reason = table.Column<string>(maxLength: 500, nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelContractForms", x => x.CancelContractFormId);
                    table.ForeignKey(
                        name: "FK_CancelContractForms_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelContractForms_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractRenewalForms",
                columns: table => new
                {
                    ContractRenewalFormId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(nullable: false),
                    StaffId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractRenewalForms", x => x.ContractRenewalFormId);
                    table.ForeignKey(
                        name: "FK_ContractRenewalForms_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractRenewalForms_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractId);
                    table.ForeignKey(
                        name: "FK_Contracts_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CancelContractForms_StaffId",
                table: "CancelContractForms",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelContractForms_StudentId",
                table: "CancelContractForms",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractRenewalForms_StaffId",
                table: "ContractRenewalForms",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractRenewalForms_StudentId",
                table: "ContractRenewalForms",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_StudentId",
                table: "Contracts",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CancelContractForms");

            migrationBuilder.DropTable(
                name: "ContractRenewalForms");

            migrationBuilder.DropTable(
                name: "Contracts");
        }
    }
}
