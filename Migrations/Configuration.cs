namespace FinancialPortal.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FinancialPortal.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<FinancialPortal.Models.ApplicationDbContext>
    {
        private SeedHelper seedHelper = new SeedHelper();
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FinancialPortal.Models.ApplicationDbContext context)
        {

            if (System.Diagnostics.Debugger.IsAttached == false)
            {
                System.Diagnostics.Debugger.Launch();
            }
            seedHelper.SeedRoles();
            seedHelper.SeedHouse();
            seedHelper.SeedUsers();
            seedHelper.AssignRoles();
            seedHelper.SeedAccountTypes();
            seedHelper.SeedBankAccounts();
            seedHelper.SeedTransactionTypes();
            seedHelper.SeedBudgets();
            seedHelper.SeedBudgetItems();
            seedHelper.SeedNotification();
            seedHelper.SeedTransactions();
        }
    }
}
    