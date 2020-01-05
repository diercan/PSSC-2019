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
        public ActionResult Menu(CUSTOMER customerModel,string history,string add, string profile)
        {
           
            if (!string.IsNullOrEmpty(history))
                return RedirectToAction("Show", "History", customerModel);
            if (!string.IsNullOrEmpty(add))
                return RedirectToAction("Index", "Trade", customerModel);
            if (!string.IsNullOrEmpty(profile))
                return RedirectToAction("Show", "History", customerModel);
            return View();



        }
    }
}