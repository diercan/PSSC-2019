using LegumeDeBelint.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LegumeDeBelint.Controllers
{
    public class AccountController : Controller
    {
        //GET: /Account/Register
        public ActionResult Register()
        {
          
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
      

    }
}