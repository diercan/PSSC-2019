using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedFind.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedFind.Controllers
{
    public class LoginStudentController : Controller
    {
        public LoginStudentController()
        {

        }
        public IActionResult Login()
        {
            return View();
        }
        

    }
}