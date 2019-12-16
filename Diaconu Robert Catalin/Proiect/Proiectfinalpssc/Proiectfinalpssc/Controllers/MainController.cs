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
        public ActionResult Menu()
        {
            return View("Menu");
        }
    }
}