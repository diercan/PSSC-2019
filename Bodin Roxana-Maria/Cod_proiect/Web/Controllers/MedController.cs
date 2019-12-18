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

namespace Web.Controllers
{
    public class MedController : Controller
    {
        private readonly ILogger<MedController> _logger;
         public MedController(ILogger<MedController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public IActionResult Index(string Date,Models.Appointment appointment)
        {
            ViewBag.HtmlStr=appointment.ShowAppointments(Date);
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
