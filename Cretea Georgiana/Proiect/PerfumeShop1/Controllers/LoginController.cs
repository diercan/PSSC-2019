using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfumeShop1.Models;

namespace PerfumeShop1.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login( )
        {
            User userModel = new User();
            return View(userModel);
        }
        [HttpPost]
        public ActionResult Login(User userModel)
        {
            using (DbModels dbModel = new DbModels())
            {
                if (dbModel.Users.Any(x => x.Username == userModel.Username && x.Password==userModel.Password))
                {
                   // ViewBag.DuplicateMessage = "Login Successfull";
                    return View("~/Views/Meniu/Meniu.cshtml");
                }
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Wrong Username or Password";
            return View("Login");
        }
    }
}