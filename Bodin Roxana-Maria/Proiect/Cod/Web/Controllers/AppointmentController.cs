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
using Web.Repositories;
using Web.Models.DDD;
using Web.Services;

namespace Web.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentService _appointmentService;
        public string med;
         public AppointmentController(ILogger<AppointmentController> logger,IAppointmentService appointmentService, IAppointmentRepository appointmentRepository)
        {
            _logger = logger;
            _appointmentRepository = appointmentRepository;
            _appointmentService=appointmentService;
        }

        [HttpPost]
        public IActionResult Index(string medName,Models.AppointmentDto app)
        {
            return View();
        }  
        [HttpPost]
        public IActionResult MadeAppointment(Models.AppointmentDto app)
        {
            if (ModelState.IsValid)
            {
                var appointment = new Models.DDD.Appointment(app);
                _appointmentRepository.CreateAppointment(appointment);
                ViewBag.HtmlStr="<p>Appointment created succesfully!</p>"; 
                return RedirectToAction("GetAppointments", "Appointment",new { Date =app.PatientName} );
            }
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetAppointments(string Id,string Date,Models.AppointmentDto appointment){
            await _appointmentService.Initialize();
            List<Appointment> app=new List<Appointment>();
            if(Date!=null)
            {
                app =await _appointmentService.GetAppointmentByPatient(Date);
            }
            else if(Id!=null)
            {
                List<Appointment> app2=new List<Appointment>();
                app2 =await _appointmentService.GetAppointmentById(Id);
                _appointmentRepository.DeleteAppointment(Id);
                app=await _appointmentService.GetAppointmentByPatient(app2[0].PartitionKey);
                Sender sender=new Sender();
                Console.WriteLine(sender.Publish(app2[0].DOB));
                return RedirectToAction("GetAppointments", "Appointment",new {Date=app2[0].PartitionKey});

            }
            return View(app);
        }
        [HttpPost]
        public IActionResult ChangeMedForAppointment(string Id,string Cancel,Models.AppointmentDto app){
            if(Id!=null)
            {
                app.Id=Id;
                return View(app);
            }
            return RedirectToAction("GetAppointments", "Appointment",new { Id=Cancel} );
        }
        [HttpPost]
        public async Task<IActionResult> UpdateMedForAppointment(string Id,Models.AppointmentDto app){
            
            List<Appointment> appointment = new List<Appointment>();
            appointment= await _appointmentRepository.GetAppointment(Id); // by id
            appointment[0].ChangeMed(app.MedName);
            _appointmentRepository.UpdateAppointment(appointment[0]);
            return RedirectToAction("GetAppointments", "Appointment",new { Date =appointment[0].PartitionKey} );
        }
        [HttpPost]
        public IActionResult ChangeStatusForAppointment(string Id,Models.AppointmentDto app){
            app.Id=Id;
            return View(app);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatusForAppointment(string Id,Models.AppointmentDto app){
            
            List<Appointment> appointment = new List<Appointment>();
            appointment= await _appointmentRepository.GetAppointment(Id); // by id
            appointment[0].ChangeState(app.Status);
            _appointmentRepository.UpdateAppointment(appointment[0]);
            return RedirectToAction("Index", "Med",new { Date =appointment[0].MedName} );
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
