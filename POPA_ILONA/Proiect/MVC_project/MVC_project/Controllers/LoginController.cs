using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_project.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC_project.Controllers
{
    public class LoginController : Controller
    {
        // GET: /Login/
        public IActionResult Index()
        {
            return View();
        } 

     

        public ActionResult Login(Login objUserLogin)
        {
            if (objUserLogin.Username == "admin" && objUserLogin.Password == "admin")
                
                return RedirectToAction("ChooseAdmin");
            else
                if (objUserLogin.Username == "user" && objUserLogin.Password == "user")

                return RedirectToAction("Choose");

            else

               
                return View("Error");
        }

        public ActionResult Choose()
        {
            return View();
        }

        public ActionResult ChooseAdmin()
        {
            return View();
        }


        public ActionResult Error()
        {
            return View();
        }
    }
}
