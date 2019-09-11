using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortal.Enumerations
{
    public enum Role
    {
        Admin = 1,
        [Display(Name = "Head Of Household")]
        HeadOfHousehold,
        Member,
        Lobbyist
    }
}