using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LegumeDeBelint.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Despre noi";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Datele de contact";
            return View();
        }
        
    }
}