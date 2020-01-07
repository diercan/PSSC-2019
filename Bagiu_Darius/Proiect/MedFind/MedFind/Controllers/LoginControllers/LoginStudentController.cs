using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedFind.Interfaces;
using MedFind.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedFind.Controllers
{
    public class LoginStudentController : Controller
    {
        private ILoginStudent _student;
        public LoginStudentController(ILoginStudent student)
        {
            _student = student;
        }
        public IActionResult Login()
        {
            return View();
        }

        public ActionResult LoginStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginStudent(LoginStudentViewModel login)
        {
            try
            {
                var model = _student.CheckLoginStudent(login);
                if (model != null)
                {
                    return View("~/Views/Student/Details.cshtml", model);
                }
                else
                    return View();
            }
            catch
            {

                return View();
            }
        }

    }
}