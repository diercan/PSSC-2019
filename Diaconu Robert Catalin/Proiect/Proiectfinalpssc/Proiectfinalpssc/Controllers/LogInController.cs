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
            var flag = 0;
            using (PSSCEntities dbModel = new PSSCEntities())
            {
                foreach (CUSTOMER cst in dbModel.CUSTOMERS)
                 // if (dbModel.CUSTOMERS.Any(cst => cst.USERNAME == customerModel.USERNAME && cst.PASSWORD == customerModel.PASSWORD ))
                    if (cst.USERNAME == customerModel.USERNAME && cst.PASSWORD == customerModel.PASSWORD)
                    {
                        flag=1;
                        customerModel = cst;
                    }
                   
                if(flag==1)
                {
                    ViewBag.LoggedMessage = "LOGGED IN";
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