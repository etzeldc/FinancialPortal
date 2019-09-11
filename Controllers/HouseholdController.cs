using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialPortal.Models;
using FinancialPortal.Helpers;
using Microsoft.AspNet.Identity;
using System.Net;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace FinancialPortal.Controllers
{
    public class HouseholdController : Controller
    {
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));
        private ApplicationDbContext db = new ApplicationDbContext();
        private HouseHelper houseHelper = new HouseHelper();

        // GET: Household
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        // POST: Household/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Greeting")] Household household)
        {
            if (ModelState.IsValid)
            {
                household.Established = DateTime.Now;
                db.Households.Add(household);
                db.SaveChanges();
                var user = db.Users.Find(User.Identity.GetUserId());
                user.HouseholdId = household.Id;
                db.SaveChanges();
                userManager.RemoveFromRole(user.Id, "Lobbyist");
                userManager.AddToRole(user.Id, "HeadOfHousehold");

                await AdminHelper.ReauthorizeUserAsync(user);
                return RedirectToAction("Setup", new { household.Id});
            }
            return View(household);
        }

        // GET: Household/Setup
        public ActionResult Setup()
        {
            return View();
        }

        ////////// POST: Household/Setup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Setup(BankAccount bankAccount, Budget budget, BudgetItem budgetItem)
        {
            try
            {
                var houseId = db.Users.Find(User.Identity.GetUserId()).Household.Id;
                if (ModelState.IsValid)
                {
                    bankAccount.HouseholdId = houseId;
                    db.BankAccounts.Add(bankAccount);

                    budget.HouseholdId = houseId;
                    db.Budgets.Add(budget);
                    db.SaveChanges();

                    budgetItem.BudgetId = budget.Id;
                    db.BudgetItems.Add(budgetItem);

                    var household = db.Households.Find(houseId);
                    household.IsConfigured = true;
                    db.SaveChanges();
                }
                return RedirectToAction("Dashboard", "Household", new { houseId });
            }
            catch
            {
                return RedirectToAction("Lobby", "Home");
            }
        }

        ////////// GET: Households/Dashboard/5
        [Authorize]
        public ActionResult Dashboard()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            if (user.HouseholdId == null)
                return RedirectToAction("Lobby", "Home");
            return View(user.Household);
        }


        ////////// GET: Households/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Household household = db.Households.Find(id);

            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        ////////// POST: Households/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,Greeting,Created")] Household household)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    var oldHouse = db.Households.AsNoTracking().FirstOrDefault(h => h.Id == household.Id);
                    var newHouse = db.Households.Find(household.Id);
                    db.Households.Attach(newHouse);
                    db.Entry(newHouse).Property(x => x.Name).IsModified = true;
                    db.Entry(newHouse).Property(x => x.Greeting).IsModified = true;
                    db.SaveChanges();
                    //Household Notification sends
                    //houseHelper.HouseNotification(oldHouse, newHouse);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        ////////// POST: Households/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #region Partial Views
        public ActionResult CreateHousePartial()
        {
            return PartialView();
        }
        public ActionResult InvitePartial()
        {   /////MODAL\\\\\
            return PartialView();
        }
        public ActionResult NotificationsPartial()
        {
            return PartialView();
        }

        public ActionResult AddBankPartial()
        {
            return PartialView();
        }
        public ActionResult BankAccountPartial()
        {
            return PartialView();
        }
        public ActionResult EditBankPartial()
        {
            return PartialView();
        }

        public ActionResult AddBudgetPartial()
        {
            return PartialView();
        }
        public ActionResult BudgetPartial()
        {
            return PartialView();
        }
        public ActionResult EditBudgetPartial()
        {
            return PartialView();
        }

        public ActionResult AddItemPartial()
        {
            return PartialView();
        }
        public ActionResult BudgetItemPartial()
        {
            return PartialView();
        }
        public ActionResult EditItemPartial()
        {
            return PartialView();
        }

        public ActionResult AddTransactionPartial()
        {   /////MODAL\\\\\
            return PartialView();
        }
        public ActionResult TransactionPartial()
        {
            return PartialView();
        }
        public ActionResult EditTransactionPartial()
        {
            return PartialView();
        }
        #endregion

        #region Trash
    

        // GET: Households/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        #endregion
    }
}
