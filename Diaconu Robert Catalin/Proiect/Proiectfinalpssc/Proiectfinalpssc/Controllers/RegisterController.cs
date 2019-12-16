using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proiectfinalpssc.Models;

namespace Proiectfinalpssc.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            CUSTOMER customerModel = new CUSTOMER();


            return View(customerModel);
        }
        [HttpPost]
        public ActionResult Register(CUSTOMER customerModel)
        {
            using (PSSCEntities dbModel = new PSSCEntities())
            {
                if(dbModel.CUSTOMERS.Any(cst=> cst.USERNAME== customerModel.USERNAME))
                {
                    ViewBag.TakenMessage = "Username already taken!";
                    return View("Register", customerModel);
                }

                dbModel.CUSTOMERS.Add(customerModel);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccesMessage = "Registration Successful";
            return View("Register", new CUSTOMER());
            
        }
    }
}