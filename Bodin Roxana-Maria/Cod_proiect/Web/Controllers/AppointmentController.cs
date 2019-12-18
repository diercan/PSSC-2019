using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Net;
using System.IO;
using Json.Net;
using Newtonsoft.Json;
using System.Globalization;

namespace Web.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ILogger<AppointmentController> _logger;
        public string med;
         public AppointmentController(ILogger<AppointmentController> logger)
        {
            _logger = logger;
        }
       
        [HttpPost]
        public IActionResult Index(string medName,Models.Appointment app)
        {
            ViewBag.HtmlStr=app.ShowDates(medName);
            return View();
        }  
        [HttpPost]
        public IActionResult MadeAppointment(Models.Appointment app)
        {
            if (ModelState.IsValid)
            {
                String mydateFormat = app.DOB.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                if(app.AddAppointment(app.MedName,mydateFormat,app.Name,app.BreedPet,app.Symptoms))
                {
                    //var json = JsonConvert.SerializeObject(app);
                   ViewBag.HtmlStr="<p>Appointment created succesfully!</p>";
                }
                else
                {
                    ViewBag.HtmlStr="<p>Appointment failed!</p>";
                    ModelState.AddModelError("", "Autentificare esuata!");
                } 
            }
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
