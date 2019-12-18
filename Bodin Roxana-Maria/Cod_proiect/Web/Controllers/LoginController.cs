using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using System.Web;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Patient(Models.Patient patient)
        {
            if (ModelState.IsValid)
            {
                if(patient.IsValid(patient.UserName, patient.Password))
                {
                    return RedirectToAction("Index", "Patient");
                }
                else
                {
                    ModelState.AddModelError("", "Autentificare esuata!");
                }
            }
            return View(patient);
        }
        [HttpPost]
        public ActionResult Med(Models.Med med)
        {
            if (ModelState.IsValid)
            {
                if(med.IsValid(med.Name, med.Password))
                {
                    return RedirectToAction("Index", "Med",new { Date =med.Name });
                }
                else
                {
                    ModelState.AddModelError("", "Autentificare esuata!");
                }
            }
            return View(med);
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
   
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
