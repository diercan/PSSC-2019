using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfumeShop1.Models;

namespace PerfumeShop1.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Add(int id=0)
        {
            User userModel = new User();
            return View(userModel);
        }
        [HttpPost]
        public ActionResult Add(User userModel)
        {
            using(DbModels dbModel = new DbModels())
            {
                if(dbModel.Users.Any(x=>x.Username==userModel.Username))
                {
                    ViewBag.DuplicateMessage = "Username already exist";
                    return View("Add", userModel);
                }
                dbModel.Users.Add(userModel);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return View("Add", new User());
        }
    }
}