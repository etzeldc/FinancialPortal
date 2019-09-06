using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FinancialPortal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int? HouseholdId { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Must be within {2} and {1} long.", MinimumLength = 1)]
        public string FirstName { get; set; }
        [StringLength(20, ErrorMessage = "Must be within {2} and {1} long.", MinimumLength = 1)]
        public string LastName { get; set; }
        [MaxLength(20)]
        public string DisplayName { get; set; }
        public string AvatarUrl { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [NotMapped]
        public string EmailWithAlias
        {
            get
            {
                return $"\'{FirstName} {LastName}\'<{Email}>";
            }
        }

        public virtual Household Household { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }

        public ApplicationUser()
        {
            Notifications = new HashSet<Notification>();
            BankAccounts = new HashSet<BankAccount>();
            Budgets = new HashSet<Budget>();
            Invitations = new HashSet<Invitation>();
        }




        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
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

        public DbSet<AccountType> AccountTypes {get;set;}
        public DbSet<BankAccount> BankAccounts {get;set;}
        public DbSet<Budget> Budgets {get;set;}
        public DbSet<BudgetItem> BudgetItems {get;set;}
        public DbSet<Household> Households {get;set;}
        public DbSet<Invitation> Invitations {get;set;}
        public DbSet<Notification> Notifications {get;set;}
        public DbSet<Transaction> Transactions {get;set;}
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<Exception> Exceptions { get; set; }
    }
}