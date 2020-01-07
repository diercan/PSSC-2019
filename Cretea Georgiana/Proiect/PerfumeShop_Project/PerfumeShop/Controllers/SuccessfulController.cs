using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PerfumeShop.Services;

namespace PerfumeShop.Controllers
{
    public class SuccessfulController : Controller
    {
        public IActionResult Index()
        {
            Send snd = new Send("Congratulations! Your order has been confirmed");
            return View();
        }
    }
}