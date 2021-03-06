﻿using DataLayer;
using System.Linq;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Member model)
        {
            if (ModelState.IsValid)
            {
                using (AMMEntities db = new AMMEntities())
                {
                    var obj = db.Members.Where(m => m.Email.Equals(model.Email) && m.Password.Equals(model.Password)).FirstOrDefault();

                    if (obj != null)
                    {
                        Session["FirstName"] = obj.FirstName.ToString();
                        return RedirectToAction("UserDashboard");
                    }
                }
            }

            return View(model);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["FirstName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}