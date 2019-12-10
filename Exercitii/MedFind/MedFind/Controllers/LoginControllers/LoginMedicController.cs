using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedFind.Interfaces.LoginInterfaces;
using MedFind.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedFind.Controllers.LoginControllers
{
    public class LoginMedicController : Controller
    {
        private ILoginMedic _medic;
        public LoginMedicController(ILoginMedic medic)
        {
            _medic = medic;
        }
        // GET: LoginMedic
        public IActionResult Login()
        {
            return View();
        }

        public ActionResult LoginMedic()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginMedic(LoginMedicViewModel login)
        {
            try
            {
                var model = _medic.CheckLoginMedic(login);
                if (model != null)
                {
                    return View("~/Views/Medic/Details.cshtml", model);
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