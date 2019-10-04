using DormyWebService.Entities.Account;
using DormyWebService.Entities.Room;
using Microsoft.EntityFrameworkCore;

namespace DormyWebService.Entities
{
    public sealed class DormyDbContext:DbContext
    {
        public DormyDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        //Register the tables in database
        public DbSet<Account.Account> Accounts { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentPriorityType> StudentPriorityTypes { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room.Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<EvaluationScoreHistory> EvaluationScoreHistories { get; set; }
        //Seed Tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Set not identity keys
            modelBuilder.Entity<Role>().Property(r => r.Id).ValueGeneratedNever();
            modelBuilder.Entity<RoomType>().Property(r => r.Id).ValueGeneratedNever();
            modelBuilder.Entity<EquipmentType>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<StudentPriorityType>().Property(e => e.Id).ValueGeneratedNever();

            //Seed Role Table
            modelBuilder.Entity<Role>().HasData(
                new Role{Id = 1, Name = "Admin"},
                new Role { Id = 2, Name = "Staff" },
                new Role { Id = 3, Name = "Student" }
            );

            //Seed Student Priority Type Table
            modelBuilder.Entity<StudentPriorityType>().HasData(
                new StudentPriorityType {Id = 1, Name = "Type 1"},
                new StudentPriorityType {Id = 2, Name = "Type 2"}
            );

            modelBuilder.Entity<Room.Room>().HasMany(r=> r.Students).WithOne(s => s.Room);
        }

    }
}