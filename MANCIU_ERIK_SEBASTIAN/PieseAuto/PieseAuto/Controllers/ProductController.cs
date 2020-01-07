using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PieseAuto.Models;

namespace PieseAuto.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private DataContext db = new DataContext();
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.products = db.Piese2.ToList();
            return View();
        }
    }
}