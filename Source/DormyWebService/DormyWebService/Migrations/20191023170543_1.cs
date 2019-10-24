using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DormyWebService.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
//            migrationBuilder.CreateTable(
//                name: "ParamTypes",
//                columns: table => new
//                {
//                    ParamTypeId = table.Column<int>(nullable: false),
//                    Name = table.Column<string>(maxLength: 20, nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_ParamTypes", x => x.ParamTypeId);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "PricePerUnits",
//                columns: table => new
//                {
//                    PricePerUnitId = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    CreatedDate = table.Column<DateTime>(nullable: false),
//                    LastUpdated = table.Column<DateTime>(nullable: false),
//                    TargetMonth = table.Column<int>(nullable: false),
//                    TargetYear = table.Column<int>(nullable: false),
//                    WaterPricePerUnit = table.Column<decimal>(nullable: false),
//                    ElectricityPricePerUnit = table.Column<decimal>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_PricePerUnits", x => x.PricePerUnitId);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "Rooms",
//                columns: table => new
//                {
//                    RoomId = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    Name = table.Column<string>(nullable: false),
//                    Description = table.Column<string>(maxLength: 100, nullable: true),
//                    CreatedDate = table.Column<DateTime>(nullable: false),
//                    LastUpdated = table.Column<DateTime>(nullable: false),
//                    Capacity = table.Column<int>(nullable: false),
//                    RoomType = table.Column<int>(nullable: false),
//                    Price = table.Column<decimal>(nullable: false),
//                    RoomStatus = table.Column<string>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "Users",
//                columns: table => new
//                {
//                    UserId = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    Email = table.Column<string>(nullable: true),
//                    Status = table.Column<string>(nullable: false),
//                    Role = table.Column<string>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Users", x => x.UserId);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "Params",
//                columns: table => new
//                {
//                    ParamId = table.Column<int>(nullable: false),
//                    ParamTypeId = table.Column<int>(nullable: false),
//                    Name = table.Column<string>(nullable: true),
//                    Value = table.Column<int>(nullable: true),
//                    TextValue = table.Column<string>(nullable: true),
//                    TimeValue = table.Column<DateTime>(nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Params", x => x.ParamId);
//                    table.ForeignKey(
//                        name: "FK_Params_ParamTypes_ParamTypeId",
//                        column: x => x.ParamTypeId,
//                        principalTable: "ParamTypes",
//                        principalColumn: "ParamTypeId",
//                        onDelete: ReferentialAction.Cascade);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "Equipments",
//                columns: table => new
//                {
//                    EquipmentId = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    Name = table.Column<string>(maxLength: 50, nullable: true),
//                    RoomId = table.Column<int>(nullable: true),
//                    ImageUrl = table.Column<string>(nullable: true),
//                    Status = table.Column<string>(nullable: false),
//                    Price = table.Column<decimal>(nullable: false),
//                    CreatedDate = table.Column<DateTime>(nullable: false),
//                    LastUpdated = table.Column<DateTime>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Equipments", x => x.EquipmentId);
//                    table.ForeignKey(
//                        name: "FK_Equipments_Rooms_RoomId",
//                        column: x => x.RoomId,
//                        principalTable: "Rooms",
//                        principalColumn: "RoomId",
//                        onDelete: ReferentialAction.Restrict);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "RoomMonthlyBills",
//                columns: table => new
//                {
//                    RoomMonthlyBillId = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    RoomId = table.Column<int>(nullable: false),
//                    IsPaid = table.Column<bool>(nullable: false),
//                    TargetMonth = table.Column<int>(nullable: false),
//                    TargetYear = table.Column<int>(nullable: false),
//                    CreatedDate = table.Column<DateTime>(nullable: false),
//                    LastUpdated = table.Column<DateTime>(nullable: false),
//                    PreviousWaterNumber = table.Column<int>(nullable: false),
//                    NewWaterNumber = table.Column<int>(nullable: false),
//                    WaterBill = table.Column<decimal>(nullable: false),
//                    PreviousElectricityNumber = table.Column<int>(nullable: false),
//                    NewElectricityNumber = table.Column<int>(nullable: false),
//                    ElectricityBill = table.Column<decimal>(nullable: false),
//                    TotalRoomFee = table.Column<decimal>(nullable: false),
//                    TotalAmount = table.Column<decimal>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_RoomMonthlyBills", x => x.RoomMonthlyBillId);
//                    table.ForeignKey(
//                        name: "FK_RoomMonthlyBills_Rooms_RoomId",
//                        column: x => x.RoomId,
//                        principalTable: "Rooms",
//                        principalColumn: "RoomId",
//                        onDelete: ReferentialAction.Cascade);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "Admins",
//                columns: table => new
//                {
//                    AdminId = table.Column<int>(nullable: false),
//                    Name = table.Column<string>(maxLength: 50, nullable: false),
//                    IdentityNumber = table.Column<string>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Admins", x => x.AdminId);
//                    table.ForeignKey(
//                        name: "FK_Admins_Users_AdminId",
//                        column: x => x.AdminId,
//                        principalTable: "Users",
//                        principalColumn: "UserId",
//                        onDelete: ReferentialAction.Cascade);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "Notifications",
//                columns: table => new
//                {
//                    NotificationId = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    OwnerUserId = table.Column<int>(nullable: true),
//                    Description = table.Column<string>(maxLength: 200, nullable: true),
//                    Url = table.Column<string>(nullable: true),
//                    Date = table.Column<DateTime>(nullable: false),
//                    LastUpdated = table.Column<DateTime>(nullable: false),
//                    Status = table.Column<int>(nullable: false),
//                    Type = table.Column<int>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
//                    table.ForeignKey(
//                        name: "FK_Notifications_Users_OwnerUserId",
//                        column: x => x.OwnerUserId,
//                        principalTable: "Users",
//                        principalColumn: "UserId",
//                        onDelete: ReferentialAction.Restrict);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "Staff",
//                columns: table => new
//                {
//                    StaffId = table.Column<int>(nullable: false),
//                    Name = table.Column<string>(maxLength: 50, nullable: false),
//                    IdentityNumber = table.Column<string>(nullable: true),
//                    HomeTown = table.Column<string>(nullable: true),
//                    Gender = table.Column<bool>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Staff", x => x.StaffId);
//                    table.ForeignKey(
//                        name: "FK_Staff_Users_StaffId",
//                        column: x => x.StaffId,
//                        principalTable: "Users",
//                        principalColumn: "UserId",
//                        onDelete: ReferentialAction.Cascade);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "Students",
//                columns: table => new
//                {
//                    StudentId = table.Column<int>(nullable: false),
//                    Name = table.Column<string>(maxLength: 50, nullable: false),
//                    StartedSchoolYear = table.Column<int>(nullable: false),
//                    IdentityNumber = table.Column<string>(maxLength: 12, nullable: false),
//                    IdentityCardImageUrl = table.Column<string>(nullable: true),
//                    StudentCardNumber = table.Column<string>(nullable: false),
//                    StudentCardImageUrl = table.Column<string>(nullable: true),
//                    Term = table.Column<int>(nullable: false),
//                    PriorityType = table.Column<int>(nullable: false),
//                    PriorityImageUrl = table.Column<string>(nullable: true),
//                    RoomId = table.Column<int>(nullable: true),
//                    Gender = table.Column<bool>(nullable: false),
//                    Address = table.Column<string>(maxLength: 100, nullable: true),
//                    PhoneNumber = table.Column<string>(nullable: true),
//                    IsRoomLeader = table.Column<bool>(nullable: false),
//                    EvaluationScore = table.Column<int>(nullable: false),
//                    AccountBalance = table.Column<decimal>(nullable: false),
//                    BirthDay = table.Column<DateTime>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Students", x => x.StudentId);
//                    table.ForeignKey(
//                        name: "FK_Students_Rooms_RoomId",
//                        column: x => x.RoomId,
//                        principalTable: "Rooms",
//                        principalColumn: "RoomId",
//                        onDelete: ReferentialAction.Restrict);
//                    table.ForeignKey(
//                        name: "FK_Students_Users_StudentId",
//                        column: x => x.StudentId,
//                        principalTable: "Users",
//                        principalColumn: "UserId",
//                        onDelete: ReferentialAction.Cascade);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "News",
//                columns: table => new
//                {
//                    NewsId = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    AuthorId = table.Column<int>(nullable: false),
//                    Title = table.Column<string>(maxLength: 100, nullable: false),
//                    Content = table.Column<string>(nullable: false),
//                    AttachedFileUrl = table.Column<string>(nullable: true),
//                    CreatedDate = table.Column<DateTime>(nullable: false),
//                    LastUpdate = table.Column<DateTime>(nullable: false),
//                    Status = table.Column<string>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_News", x => x.NewsId);
//                    table.ForeignKey(
//                        name: "FK_News_Admins_AuthorId",
//                        column: x => x.AuthorId,
//                        principalTable: "Admins",
//                        principalColumn: "AdminId",
//                        onDelete: ReferentialAction.Cascade);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "CancelContractForms",
//                columns: table => new
//                {
//                    CancelContractFormId = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    CreatedDate = table.Column<DateTime>(nullable: false),
//                    LastUpdated = table.Column<DateTime>(nullable: false),
//                    CancelationDate = table.Column<DateTime>(nullable: false),
//                    StudentId = table.Column<int>(nullable: false),
//                    StaffId = table.Column<int>(nullable: true),
//                    Reason = table.Column<string>(maxLength: 500, nullable: false),
//                    Status = table.Column<int>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_CancelContractForms", x => x.CancelContractFormId);
//                    table.ForeignKey(
//                        name: "FK_CancelContractForms_Staff_StaffId",
//                        column: x => x.StaffId,
//                        principalTable: "Staff",
//                        principalColumn: "StaffId",
//                        onDelete: ReferentialAction.Restrict);
//                    table.ForeignKey(
//                        name: "FK_CancelContractForms_Students_StudentId",
//                        column: x => x.StudentId,
//                        principalTable: "Students",
//                        principalColumn: "StudentId",
//                        onDelete: ReferentialAction.Cascade);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "Contract",
//                columns: table => new
//                {
//                    ContractId = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    StudentId = table.Column<int>(nullable: false),
//                    Status = table.Column<string>(nullable: false),
//                    CreatedDate = table.Column<DateTime>(nullable: false),
//                    LastUpdate = table.Column<DateTime>(nullable: false),
//                    StartDate = table.Column<DateTime>(nullable: false),
//                    EndDate = table.Column<DateTime>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Contract", x => x.ContractId);
//                    table.ForeignKey(
//                        name: "FK_Contract_Students_StudentId",
//                        column: x => x.StudentId,
//                        principalTable: "Students",
//                        principalColumn: "StudentId",
//                        onDelete: ReferentialAction.Restrict);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "ContractRenewalForms",
//                columns: table => new
//                {
//                    ContractRenewalFormId = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    StudentId = table.Column<int>(nullable: false),
//                    StaffId = table.Column<int>(nullable: true),
//                    Status = table.Column<int>(nullable: false),
//                    Month = table.Column<int>(nullable: false),
//                    StartTime = table.Column<DateTime>(nullable: false),
//                    EndTime = table.Column<DateTime>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_ContractRenewalForms", x => x.ContractRenewalFormId);
//                    table.ForeignKey(
//                        name: "FK_ContractRenewalForms_Staff_StaffId",
//                        column: x => x.StaffId,
//                        principalTable: "Staff",
//                        principalColumn: "StaffId",
//                        onDelete: ReferentialAction.Restrict);
//                    table.ForeignKey(
//                        name: "FK_ContractRenewalForms_Students_StudentId",
//                        column: x => x.StudentId,
//                        principalTable: "Students",
//                        principalColumn: "StudentId",
//                        onDelete: ReferentialAction.Cascade);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "IssueTickets",
//                columns: table => new
//                {
//                    IssueTicketId = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    Type = table.Column<int>(nullable: false),
//                    Status = table.Column<int>(nullable: false),
//                    OwnerUserId = table.Column<int>(nullable: false),
//                    TargetUserUserId = table.Column<int>(nullable: true),
//                    EquipmentId = table.Column<int>(nullable: true),
//                    StaffId = table.Column<int>(nullable: true),
//                    Title = table.Column<string>(maxLength: 100, nullable: false),
//                    Description = table.Column<string>(nullable: true),
//                    Point = table.Column<int>(nullable: false),
//                    ImageUrl = table.Column<string>(nullable: true),
//                    RoomId = table.Column<int>(nullable: true),
//                    CreatedDate = table.Column<DateTime>(nullable: false),
//                    LastUpdated = table.Column<DateTime>(nullable: false),
//                    StudentId = table.Column<int>(nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_IssueTickets", x => x.IssueTicketId);
//                    table.ForeignKey(
//                        name: "FK_IssueTickets_Equipments_EquipmentId",
//                        column: x => x.EquipmentId,
//                        principalTable: "Equipments",
//                        principalColumn: "EquipmentId",
//                        onDelete: ReferentialAction.Restrict);
//                    table.ForeignKey(
//                        name: "FK_IssueTickets_Users_OwnerUserId",
//                        column: x => x.OwnerUserId,
//                        principalTable: "Users",
//                        principalColumn: "UserId",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "FK_IssueTickets_Rooms_RoomId",
//                        column: x => x.RoomId,
//                        principalTable: "Rooms",
//                        principalColumn: "RoomId",
//                        onDelete: ReferentialAction.Restrict);
//                    table.ForeignKey(
//                        name: "FK_IssueTickets_Staff_StaffId",
//                        column: x => x.StaffId,
//                        principalTable: "Staff",
//                        principalColumn: "StaffId",
//                        onDelete: ReferentialAction.Restrict);
//                    table.ForeignKey(
//                        name: "FK_IssueTickets_Students_StudentId",
//                        column: x => x.StudentId,
//                        principalTable: "Students",
//                        principalColumn: "StudentId",
//                        onDelete: ReferentialAction.Restrict);
//                    table.ForeignKey(
//                        name: "FK_IssueTickets_Users_TargetUserUserId",
//                        column: x => x.TargetUserUserId,
//                        principalTable: "Users",
//                        principalColumn: "UserId",
//                        onDelete: ReferentialAction.Restrict);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "MoneyTransactions",
//                columns: table => new
//                {
//                    MoneyTransactionId = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    StudentId = table.Column<int>(nullable: false),
//                    Type = table.Column<int>(nullable: false),
//                    RoomId = table.Column<int>(nullable: false),
//                    OriginalBalance = table.Column<decimal>(nullable: false),
//                    MoneyAmount = table.Column<decimal>(nullable: false),
//                    ResultBalance = table.Column<decimal>(nullable: false),
//                    Date = table.Column<DateTime>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_MoneyTransactions", x => x.MoneyTransactionId);
//                    table.ForeignKey(
//                        name: "FK_MoneyTransactions_Rooms_RoomId",
//                        column: x => x.RoomId,
//                        principalTable: "Rooms",
//                        principalColumn: "RoomId",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "FK_MoneyTransactions_Students_StudentId",
//                        column: x => x.StudentId,
//                        principalTable: "Students",
//                        principalColumn: "StudentId",
//                        onDelete: ReferentialAction.Cascade);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "RoomBookingRequestForm",
//                columns: table => new
//                {
//                    RoomBookingRequestFormId = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    CreatedDate = table.Column<DateTime>(nullable: false),
//                    LastUpdated = table.Column<DateTime>(nullable: false),
//                    Month = table.Column<int>(nullable: false),
//                    Status = table.Column<string>(nullable: false),
//                    StudentId = table.Column<int>(nullable: false),
//                    StaffId = table.Column<int>(nullable: true),
//                    TargetRoomType = table.Column<int>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_RoomBookingRequestForm", x => x.RoomBookingRequestFormId);
//                    table.ForeignKey(
//                        name: "FK_RoomBookingRequestForm_Staff_StaffId",
//                        column: x => x.StaffId,
//                        principalTable: "Staff",
//                        principalColumn: "StaffId",
//                        onDelete: ReferentialAction.Restrict);
//                    table.ForeignKey(
//                        name: "FK_RoomBookingRequestForm_Students_StudentId",
//                        column: x => x.StudentId,
//                        principalTable: "Students",
//                        principalColumn: "StudentId",
//                        onDelete: ReferentialAction.Cascade);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "RoomTransferRequestForms",
//                columns: table => new
//                {
//                    RoomTransferRequestFormId = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    CreatedDate = table.Column<DateTime>(nullable: false),
//                    LastUpdated = table.Column<DateTime>(nullable: false),
//                    Reason = table.Column<string>(maxLength: 500, nullable: false),
//                    StudentId = table.Column<int>(nullable: false),
//                    StaffId = table.Column<int>(nullable: true),
//                    TargetRoomType = table.Column<int>(nullable: false),
//                    Status = table.Column<int>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_RoomTransferRequestForms", x => x.RoomTransferRequestFormId);
//                    table.ForeignKey(
//                        name: "FK_RoomTransferRequestForms_Staff_StaffId",
//                        column: x => x.StaffId,
//                        principalTable: "Staff",
//                        principalColumn: "StaffId",
//                        onDelete: ReferentialAction.Restrict);
//                    table.ForeignKey(
//                        name: "FK_RoomTransferRequestForms_Students_StudentId",
//                        column: x => x.StudentId,
//                        principalTable: "Students",
//                        principalColumn: "StudentId",
//                        onDelete: ReferentialAction.Cascade);
//                });
//
//            migrationBuilder.CreateTable(
//                name: "StudentMonthlyBills",
//                columns: table => new
//                {
//                    StudentMonthlyBillId = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    IsPaid = table.Column<bool>(nullable: false),
//                    StudentId = table.Column<int>(nullable: false),
//                    RoomId = table.Column<int>(nullable: false),
//                    RoomMonthlyBillId = table.Column<int>(nullable: false),
//                    RoomUtilityFee = table.Column<decimal>(nullable: false),
//                    Percentage = table.Column<decimal>(nullable: false),
//                    UtilityFee = table.Column<decimal>(nullable: false),
//                    RoomFee = table.Column<decimal>(nullable: false),
//                    Total = table.Column<decimal>(nullable: false),
//                    TargetMonth = table.Column<DateTime>(nullable: false),
//                    TargetYear = table.Column<DateTime>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_StudentMonthlyBills", x => x.StudentMonthlyBillId);
//                    table.ForeignKey(
//                        name: "FK_StudentMonthlyBills_Rooms_RoomId",
//                        column: x => x.RoomId,
//                        principalTable: "Rooms",
//                        principalColumn: "RoomId",
//                        onDelete: ReferentialAction.Restrict);
//                    table.ForeignKey(
//                        name: "FK_StudentMonthlyBills_RoomMonthlyBills_RoomMonthlyBillId",
//                        column: x => x.RoomMonthlyBillId,
//                        principalTable: "RoomMonthlyBills",
//                        principalColumn: "RoomMonthlyBillId",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "FK_StudentMonthlyBills_Students_StudentId",
//                        column: x => x.StudentId,
//                        principalTable: "Students",
//                        principalColumn: "StudentId",
//                        onDelete: ReferentialAction.Cascade);
//                });
//
//            migrationBuilder.InsertData(
//                table: "ParamTypes",
//                columns: new[] { "ParamTypeId", "Name" },
//                values: new object[,]
//                {
//                    { 0, "MoneyTransactionType" },
//                    { 1, "NotificationType" },
//                    { 2, "StudentPriorityType" },
//                    { 3, "AcceptedEmailHost" },
//                    { 4, "RoomType" }
//                });
//
//            migrationBuilder.InsertData(
//                table: "Params",
//                columns: new[] { "ParamId", "Name", "ParamTypeId", "TextValue", "TimeValue", "Value" },
//                values: new object[,]
//                {
//                    { 0, "Priority Type 1", 2, null, null, null },
//                    { 1, "Priority Type 2", 2, null, null, null },
//                    { 2, "None", 2, null, null, null },
//                    { 10, "Fpt email host", 3, "fpt.edu.vn", null, null },
//                    { 11, "Standard Room", 4, null, null, null },
//                    { 12, "Service Room", 4, null, null, null }
//                });
//
//            migrationBuilder.CreateIndex(
//                name: "IX_CancelContractForms_StaffId",
//                table: "CancelContractForms",
//                column: "StaffId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_CancelContractForms_StudentId",
//                table: "CancelContractForms",
//                column: "StudentId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_Contract_StudentId",
//                table: "Contract",
//                column: "StudentId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_ContractRenewalForms_StaffId",
//                table: "ContractRenewalForms",
//                column: "StaffId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_ContractRenewalForms_StudentId",
//                table: "ContractRenewalForms",
//                column: "StudentId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_Equipments_RoomId",
//                table: "Equipments",
//                column: "RoomId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_IssueTickets_EquipmentId",
//                table: "IssueTickets",
//                column: "EquipmentId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_IssueTickets_OwnerUserId",
//                table: "IssueTickets",
//                column: "OwnerUserId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_IssueTickets_RoomId",
//                table: "IssueTickets",
//                column: "RoomId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_IssueTickets_StaffId",
//                table: "IssueTickets",
//                column: "StaffId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_IssueTickets_StudentId",
//                table: "IssueTickets",
//                column: "StudentId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_IssueTickets_TargetUserUserId",
//                table: "IssueTickets",
//                column: "TargetUserUserId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_MoneyTransactions_RoomId",
//                table: "MoneyTransactions",
//                column: "RoomId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_MoneyTransactions_StudentId",
//                table: "MoneyTransactions",
//                column: "StudentId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_News_AuthorId",
//                table: "News",
//                column: "AuthorId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_Notifications_OwnerUserId",
//                table: "Notifications",
//                column: "OwnerUserId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_Params_ParamTypeId",
//                table: "Params",
//                column: "ParamTypeId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_RoomBookingRequestForm_StaffId",
//                table: "RoomBookingRequestForm",
//                column: "StaffId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_RoomBookingRequestForm_StudentId",
//                table: "RoomBookingRequestForm",
//                column: "StudentId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_RoomMonthlyBills_RoomId",
//                table: "RoomMonthlyBills",
//                column: "RoomId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_RoomTransferRequestForms_StaffId",
//                table: "RoomTransferRequestForms",
//                column: "StaffId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_RoomTransferRequestForms_StudentId",
//                table: "RoomTransferRequestForms",
//                column: "StudentId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_StudentMonthlyBills_RoomId",
//                table: "StudentMonthlyBills",
//                column: "RoomId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_StudentMonthlyBills_RoomMonthlyBillId",
//                table: "StudentMonthlyBills",
//                column: "RoomMonthlyBillId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_StudentMonthlyBills_StudentId",
//                table: "StudentMonthlyBills",
//                column: "StudentId");
//
//            migrationBuilder.CreateIndex(
//                name: "IX_Students_RoomId",
//                table: "Students",
//                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CancelContractForms");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "ContractRenewalForms");

            migrationBuilder.DropTable(
                name: "IssueTickets");

            migrationBuilder.DropTable(
                name: "MoneyTransactions");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Params");

            migrationBuilder.DropTable(
                name: "PricePerUnits");

            migrationBuilder.DropTable(
                name: "RoomBookingRequestForm");

            migrationBuilder.DropTable(
                name: "RoomTransferRequestForms");

            migrationBuilder.DropTable(
                name: "StudentMonthlyBills");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "ParamTypes");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "RoomMonthlyBills");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
