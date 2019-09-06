using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FinancialPortal.Helpers
{
    public class HouseHelper
    {
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        private static ApplicationDbContext db = new ApplicationDbContext();

        //****************** WORK IN PROGRASS ******************\\
        //public void HouseNotification(Household oldHouse, Household newHouse)
        //{
        //    var editor = userManager.FindById(HttpContext.Current.User.Identity.GetUserId()).DisplayName;
        //    var messageBody = new StringBuilder();
        //    if (oldHouse.Name != newHouse.Name)
        //        messageBody.AppendLine($"{editor} changed the name of your household to {newHouse.Name}.");
        //    if (oldHouse.Greeting != newHouse.Greeting)
        //        messageBody.AppendLine($"{editor} changed your household's greeting to {newHouse.Greeting}.");
        //    foreach(var member in newHouse.Members)
        //    {
        //        if (editor != member)
        //        {
        //            var notification = new Notification
        //            {
        //                HouseholdId = newHouse.Id,
        //                Created = DateTime.Now,
        //                Owner = editor,
        //                Subject = $"{editor} made a change.",
        //                Body = messageBody.ToString(),
        //                Read = false,
        //            };
        //        }
        //    }
        //}
        //****************** WORK IN PROGRASS ******************\\

    }
}