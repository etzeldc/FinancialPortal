using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialPortal.Models;
using FinancialPortal.Helpers;
using Microsoft.AspNet.Identity;
using System.Net;

namespace FinancialPortal.Controllers
{
    public class HouseholdController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private HouseHelper houseHelper = new HouseHelper();

        // GET: Households
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        ////////// GET: Households/Dashboard/5
        [Authorize]
        public ActionResult Dashboard(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Household household = db.Households.Find(id);
            if (household == null)
                return HttpNotFound();
            return View(household);
        }

        ////////// POST: Households/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,Greeting")] Household household)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var creator = db.Users.Find(User.Identity.GetUserId());
                    household.Established = DateTime.Now;
                    household.Members.Add(creator);
                    db.Households.Add(household);
                    db.SaveChanges();
                }
                // REDIRECT TO A WIZARD SETUP HERE
                return RedirectToAction("Dashboard", "Household", new { household.Id });
            }
            catch
            {
                return RedirectToAction("Lobby", "Home");
            }
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
        // GET: Households/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        #endregion
    }
}
