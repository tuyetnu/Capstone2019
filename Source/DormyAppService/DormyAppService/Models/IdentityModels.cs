using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using DormyAppService.Models.AccountModels;
using DormyAppService.Models.DocumentModels;
using DormyAppService.Models.MoneyModels;
using DormyAppService.Models.NotificationModels;
using DormyAppService.Models.RoomModels;
using DormyAppService.Models.TicketModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace DormyAppService.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();

        }

        public DbSet<RoomRequest> RoomTransferRequests { get; set; }
        public DbSet<ContractRequest> ContractRequests { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<MoneyTransaction> MoneyTransactions { get; set; }
        public DbSet<MonthlyBill> MonthlyBills { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<IssueTicket> IssueTickets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

//            modelBuilder.Entity<RoomTransferRequest>()
//                .HasRequired(f => f.Status)
//                .WithRequiredDependent()
//                .WillCascadeOnDelete(false);
        }
    }


}