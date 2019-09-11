using FinancialPortal.Models;
using FinancialPortal.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class DashboardViewModel
    {
        //the household the dashboard references
        public Household household = new Household();
        //needed for form submission
        public BankAccount bankAccount = new BankAccount();
        public Budget budget = new Budget();
        public BudgetItem budgetItem = new BudgetItem();
    }
}