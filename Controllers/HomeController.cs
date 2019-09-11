using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialPortal.Helpers;
using FinancialPortal.Models;
using FinancialPortal.ViewModels;

namespace FinancialPortal.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Lobby()
        {
            return View();
        }

        [Authorize]
        public ActionResult Settings()
        {
            return View();
        }
        public ActionResult UpdateUserPartial()
        {
            return PartialView();
        }

        [Authorize]
        public ActionResult UpdateUser([Bind(Include = "Id,DisplayName,FirstName,LastName,Email,AvatarUrl")] UpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(model.Id);
                user.DisplayName = model.DisplayName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.Email;
                user.Email = model.Email;
                db.SaveChanges();
                return RedirectToAction("Settings", new { user.Id });
            }
            return View(model);
        }

        [Authorize]
        public ActionResult UpdateAvatar(UpdateViewModel model, HttpPostedFileBase avatar)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(model.Id);
                //Validator
                if (FileHelper.IsWebFriendlyImage(avatar))
                {
                    var fileName = Path.GetFileName(avatar.FileName);
                    avatar.SaveAs(Path.Combine(Server.MapPath("~/Images/"), fileName));
                    user.AvatarUrl = "/Images/" + fileName;
                    db.SaveChanges();
                }

                return RedirectToAction("Settings", new { user.Id });
            }
            return View(model);
        }
    }
}