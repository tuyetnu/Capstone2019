﻿// <auto-generated />
using System;
using DormyWebService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DormyWebService.Migrations
{
    [DbContext(typeof(DormyDbContext))]
    partial class DormyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DormyWebService.Entities.Account.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("Gender");

                    b.Property<string>("IdPictureUrl");

                    b.Property<string>("IdentityCardNumber");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("DormyWebService.Entities.Account.Staff", b =>
                {
                    b.Property<int>("AccountId");

                    b.Property<int?>("AccountId1");

                    b.Property<bool>("Gender");

                    b.Property<string>("HomeTown");

                    b.Property<string>("IdentityCardId");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("AccountId");

                    b.HasIndex("AccountId1");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("DormyWebService.Entities.Account.Student", b =>
                {
                    b.Property<int>("AccountId");

                    b.Property<int?>("AccountId1");

                    b.Property<bool>("IsRoomLeader");

                    b.Property<int?>("PriorityTypeId");

                    b.Property<int?>("RoomId");

                    b.Property<int>("StartedSchoolYear");

                    b.Property<string>("StudentCardPictureUrl");

                    b.Property<int>("Term");

                    b.HasKey("AccountId");

                    b.HasIndex("AccountId1");

                    b.HasIndex("PriorityTypeId");

                    b.HasIndex("RoomId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("DormyWebService.Entities.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EquipmentTypeId");

                    b.Property<int>("RoomId");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentTypeId");

                    b.HasIndex("RoomId");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("DormyWebService.Entities.EquipmentType", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("EquipmentTypes");
                });

            modelBuilder.Entity("DormyWebService.Entities.EvaluationScoreHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<int>("ResultedScore");

                    b.Property<int>("TargetStudentAccountId");

                    b.HasKey("Id");

                    b.HasIndex("TargetStudentAccountId");

                    b.ToTable("EvaluationScoreHistories");
                });

            modelBuilder.Entity("DormyWebService.Entities.Role", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Staff"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Student"
                        });
                });

            modelBuilder.Entity("DormyWebService.Entities.Room.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name");

                    b.Property<int?>("RoomLeaderAccountId");

                    b.Property<int?>("RoomTypeId");

                    b.HasKey("Id");

                    b.HasIndex("RoomLeaderAccountId");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("DormyWebService.Entities.Room.RoomType", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("DefaultCapacity");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("RoomTypes");
                });

            modelBuilder.Entity("DormyWebService.Entities.StudentPriorityType", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("StudentPriorityTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Type 1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Type 2"
                        });
                });

            modelBuilder.Entity("DormyWebService.Entities.Account.Account", b =>
                {
                    b.HasOne("DormyWebService.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DormyWebService.Entities.Account.Staff", b =>
                {
                    b.HasOne("DormyWebService.Entities.Account.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId1");
                });

            modelBuilder.Entity("DormyWebService.Entities.Account.Student", b =>
                {
                    b.HasOne("DormyWebService.Entities.Account.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId1");

                    b.HasOne("DormyWebService.Entities.StudentPriorityType", "PriorityType")
                        .WithMany()
                        .HasForeignKey("PriorityTypeId");

                    b.HasOne("DormyWebService.Entities.Room.Room", "Room")
                        .WithMany("Students")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("DormyWebService.Entities.Equipment", b =>
                {
                    b.HasOne("DormyWebService.Entities.EquipmentType", "EquipmentType")
                        .WithMany()
                        .HasForeignKey("EquipmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DormyWebService.Entities.Room.Room", "Room")
                        .WithMany("Equipments")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DormyWebService.Entities.EvaluationScoreHistory", b =>
                {
                    b.HasOne("DormyWebService.Entities.Account.Student", "TargetStudent")
                        .WithMany()
                        .HasForeignKey("TargetStudentAccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DormyWebService.Entities.Room.Room", b =>
                {
                    b.HasOne("DormyWebService.Entities.Account.Student", "RoomLeader")
                        .WithMany()
                        .HasForeignKey("RoomLeaderAccountId");

                    b.HasOne("DormyWebService.Entities.Room.RoomType", "RoomType")
                        .WithMany()
                        .HasForeignKey("RoomTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
