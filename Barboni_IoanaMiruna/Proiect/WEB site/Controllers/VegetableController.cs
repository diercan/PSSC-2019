using LegumeDeBelint.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LegumeDeBelint.Controllers
{
    public class VegetableController : Controller
    {
        // GET: Vegetable
        public ActionResult Vegetable()
        {
            vegetableModel vegetableModel = new vegetableModel();
            ViewBag.vegetables = vegetableModel.findAll();
            return View();
        }
    }
}

