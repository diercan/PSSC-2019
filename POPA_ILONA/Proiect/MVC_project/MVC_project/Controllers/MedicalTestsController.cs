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
    public class MedicalTestsController : Controller
    {

        private readonly IMedicalTestsRepository medicalTestsRepository;


        public MedicalTestsController(IMedicalTestsRepository medicalTestsRepository)
        {
            this.medicalTestsRepository = medicalTestsRepository;
        }

        // GET: /MedicalTests/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /MedicalTests/
        public IActionResult List2()
        {
            return View();
        }

        // GET: /MedicalTests/List
        public IActionResult List()
        {
            return View(medicalTestsRepository.GetAllMedicalTests());
        }
        
        // GET: MedicalTests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicalTests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicalTests medicalTests)
        {
            try
            {
                medicalTests.Id = Guid.NewGuid();
                medicalTestsRepository.CreateMedicalTests(medicalTests);
             
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: MedicalTests/Edit/5
        public ActionResult Edit(Guid id)
        {
            var medicalTests = medicalTestsRepository.GetMedicalTestsById(id);
            return View(medicalTests);

           
        }

        // POST: MedicalTests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, MedicalTests NewMedicalTest)
        {
            

            try
            {
                           
                medicalTestsRepository.EditMedicalTests(NewMedicalTest);

                return RedirectToAction(nameof(List)); 
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(Guid id)
        {
            var medicalTests = medicalTestsRepository.GetMedicalTestsById(id);
            return View(medicalTests);
        }

        // POST: Appointments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var medicalTests = medicalTestsRepository.GetMedicalTestsById(id);
                medicalTestsRepository.DeleteMedicalTests(medicalTests); 

                return RedirectToAction(nameof(List)); //Index 
            }
            catch
            {
                return View();
            }
        }
    }


    
}
