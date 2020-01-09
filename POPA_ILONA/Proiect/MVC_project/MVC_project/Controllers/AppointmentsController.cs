using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_project.Models;
using MVC_project.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC_project.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsRepository appointmentsRepository;

        

        public AppointmentsController(IAppointmentsRepository appointmentsRepository)
        {
            this.appointmentsRepository = appointmentsRepository;
        }
        
        // GET: Appointments
        public ActionResult Index()
        {
            return View(appointmentsRepository.GetAllAppointments());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Appointments appointments)
        {
            try
            {
                appointments.Id = Guid.NewGuid();
                appointmentsRepository.CreateAppointments(appointments);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Appointments/Create2
        public ActionResult Create2()
        {
            return View();
        }

        // POST: Appointments/Create2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(Appointments appointments)
        {
            try
            {
                appointments.Id = Guid.NewGuid();
                appointmentsRepository.CreateAppointments(appointments);

                return RedirectToAction("Confirmation");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Confirmation()
        {
            return View();
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(Guid id)
        {
            var appointments = appointmentsRepository.GetAppointmentsById(id);
            return View(appointments);
            
        }



        // POST: Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Appointments NewAppointment)
        {
            try
            {
               

               
                appointmentsRepository.EditAppointments(NewAppointment);

                return RedirectToAction(nameof(Index));
            }
            catch 
            {
                
                return View();
            }
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(Guid id)
        {
            var appointments = appointmentsRepository.GetAppointmentsById(id);
            return View(appointments);
        }

        // POST: Appointments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var appointments = appointmentsRepository.GetAppointmentsById(id);
                appointmentsRepository.DeleteAppointments(appointments);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
    }
}
