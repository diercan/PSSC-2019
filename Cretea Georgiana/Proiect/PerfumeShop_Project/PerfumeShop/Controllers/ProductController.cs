using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerfumeShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace PerfumeShop.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private DataContext db = new DataContext();
        [Route("index")]
        public IActionResult Index()
        { 
            ViewBag.products = db.Product.ToList();
            return View();
        }
    }
}