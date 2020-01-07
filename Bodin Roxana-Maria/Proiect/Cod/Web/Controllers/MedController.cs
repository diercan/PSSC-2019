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
using Web.Services;
using Web.Models.DDD;

namespace Web.Controllers
{
    public class MedController : Controller
    {
        private readonly ILogger<MedController> _logger;
        private readonly IAppointmentService _appointmentService; 
         public MedController(ILogger<MedController> logger,IAppointmentService appointmentService)
        {
            _logger = logger;
            _appointmentService=appointmentService;
        }
        
        [HttpGet]
        public async Task<ActionResult> Index(string Date,Models.AppointmentDto appointment)
        {
            await _appointmentService.Initialize();
            List<Appointment> app =await _appointmentService.GetAppointmentByMed(Date);
            return View(app);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
