using Proiectfinalpssc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiectfinalpssc.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Menu(CUSTOMER customerModel,string submit)
        {
            submit = "history";
            switch (submit)
            {
                case "history":
                    return RedirectToAction("Show", "History", customerModel);
             
                case "add":
                    return View(); 
                case "profile":
                    return View();
               
                default:
                    return View();
            }



            
            //var IBAN = customerModel.IBAN;
            //ViewBag.Message = IBAN;

        }
    }
}