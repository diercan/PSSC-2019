using Proiectfinalpssc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiectfinalpssc.Controllers
{
    public class LogInController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            CUSTOMER customerModel = new CUSTOMER();


            return View(customerModel);
        }
        [HttpPost]
        public ActionResult Login(CUSTOMER customerModel)
        {
            using (PSSCEntities dbModel = new PSSCEntities())
            {
                if (dbModel.CUSTOMERS.Any(cst => cst.USERNAME == customerModel.USERNAME && cst.PASSWORD == customerModel.PASSWORD))
                {
     
                    ViewBag.LoggedMessage = "LOGGED IN";
                    //return View("Menu", customerModel);
                    return RedirectToAction("Menu", "Main", customerModel);
                }
                else
                {
                    ViewBag.IncorrectMessage = "Incorrect user or password";
                    return View("Login", customerModel);
                    //return Content("eroare");
                   


                }


                
            } 

        }
    }
}