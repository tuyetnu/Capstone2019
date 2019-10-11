using DormyWebService.Models.AccountModels;
using DormyWebService.Models.EquipmentModels;
using DormyWebService.Models.RoomModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DormyWebService.Models
{
    public sealed class DormyDbContext:IdentityDbContext<User,Role,int>
    {
        public DormyDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        //Register the tables in database
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentPriorityType> StudentPriorityTypes { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<EvaluationScoreHistory> EvaluationScoreHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Set not identity keys
            modelBuilder.Entity<RoomType>().Property(r => r.Id).ValueGeneratedNever();
            modelBuilder.Entity<EquipmentType>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<StudentPriorityType>().Property(e => e.Id).ValueGeneratedNever();

            //Seed Student Priority Type Table
            modelBuilder.Entity<StudentPriorityType>().HasData(
                new StudentPriorityType {Id = 1, Name = "Type 1"},
                new StudentPriorityType {Id = 2, Name = "Type 2"}
            );

        }

    }
}