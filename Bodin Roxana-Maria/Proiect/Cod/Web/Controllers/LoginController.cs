using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using Web.Services;
using System.Web;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IPatientService _patientService; 
        private readonly IMedService _medService; 

        public LoginController(ILogger<LoginController> logger, IPatientService patientService, IMedService medService)
        {
            _logger = logger;
            _patientService=patientService;
            _medService=medService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Patient(Models.Patient patient)
        {
            if (ModelState.IsValid)
            {
                
                await _patientService.Initialize();
                //PatientEntity patient1=new PatientEntity("roxanabodin","delfin"){ Name="Bodin Roxana",Email="bodinroxana@yahoo.com",Phone="0771242729"};
                //await service.AddPatient(patient1);
                List<PatientEntity> p=await _patientService.GetPatientByUserName(patient.UserName);
                if(p.Count!=0 && p[0].RowKey==patient.Password)
                    return RedirectToAction("GetAppointments", "Appointment",new { Date =p[0].Name});
                else
                    ModelState.AddModelError("", "Autentificare esuata!");
                
            }
                
            return View(patient);
        }
        [HttpPost]
        public async Task<ActionResult> Med(Models.Med med)
        {
            if (ModelState.IsValid)
            {
                
                await _medService.Initialize();
                //PatientEntity patient1=new PatientEntity("roxanabodin","delfin"){ Name="Bodin Roxana",Email="bodinroxana@yahoo.com",Phone="0771242729"};
                //await service.AddPatient(patient1);
                List<MedEntity> p=await _medService.GetMedByUserName(med.Name);
                if(p.Count!=0 && p[0].RowKey==med.Password)
                    return RedirectToAction("Index", "Med",new { Date =med.Name} );
                else
                    ModelState.AddModelError("", "Autentificare esuata!");
                
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
