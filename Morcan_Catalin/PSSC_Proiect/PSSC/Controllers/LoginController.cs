using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSSC.Repository;
using PSSC.Services;

namespace PSSC.Controllers
{
    public class LoginController : Controller
    {
        ILoginService userService;

        public LoginController(ILoginService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            PSSC.Models.User user = new PSSC.Models.User();
            return View(user);
        }

        [HttpPost]
        public IActionResult Login(PSSC.Models.User user)
        {
            UserRepository userRepo = new UserRepository();
            if (userRepo.GetUser(user.Username,user.Password) != null)
            {
                ViewBag.LoggedMessage = "Logged";
                return RedirectToAction("Index", "Postare");
            }
            else
            {
                PSSC.Models.User u = new PSSC.Models.User();
                return View(user);
            }

        }

    }
}