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
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }

        //Seed Tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seed Role Table
            modelBuilder.Entity<Role>().HasData(
                new Role{Id = 1, Name = "Admin"},
                new Role { Id = 2, Name = "Staff" },
                new Role { Id = 3, Name = "Student" }
            );
        }

    }
}