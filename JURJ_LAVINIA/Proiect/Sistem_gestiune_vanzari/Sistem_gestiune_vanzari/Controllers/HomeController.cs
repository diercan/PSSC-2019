using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sistem_gestiune_vanzari.Database;
using Sistem_gestiune_vanzari.Models;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;

using System.Web.Services.Description;
namespace Sistem_gestiune_vanzari.Controllers

{
    public class HomeController : Controller
    {
        public ActionResult Index(string cautare)
        {
            InterfataUtilizatorModel model = new InterfataUtilizatorModel();

            return View(model.CreateModel(cautare));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
       
    }
}