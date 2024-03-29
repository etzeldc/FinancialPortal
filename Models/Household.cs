﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class Household
    {
        public int Id { get; set; }
        [StringLength(40)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Greeting { get; set; }
        public DateTime Established { get; set; }
        public bool IsConfigured { get; set; }
        public virtual ICollection<ApplicationUser> Members { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        public virtual ICollection<Budget> Budgets {get; set;}
        public virtual ICollection<Invitation> Invitations {get; set;}
        public virtual ICollection<Notification> Notifications {get; set;}

        public Household()
        {
            BankAccounts = new HashSet<BankAccount>();
            Budgets = new HashSet<Budget>();
            Invitations = new HashSet<Invitation>();
            Notifications = new HashSet<Notification>();
            Members = new HashSet<ApplicationUser>();
        }
    }
}