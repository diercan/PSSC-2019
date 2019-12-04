using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proiect_PSSC.Models;
using Proiect_PSSC.ViewModels;

namespace Proiect_PSSC.Controllers
{

    public class HomeController : Controller
    {

        private readonly IPacientRepository _pacientRepository;

       
        public HomeController(IPacientRepository pacientRepository)
        {
            _pacientRepository = pacientRepository;
        }


        public ViewResult Index()
        {
            var model = _pacientRepository.GetAllPacient();
            return View(model);
        }
        
        public ViewResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Pacient = _pacientRepository.GetPacient(id?? 1),
                PageTitle = "Lista analize"
            };

            return View(homeDetailsViewModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }


        public IActionResult Update(PacientiEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Pacient pacient = _pacientRepository.GetPacient(model.Id);
                pacient.Nume = model.Nume;
                pacient.Prenume = model.Prenume;
                pacient.CNP = model.CNP;
                pacient.Sexul = model.Sexul;
                pacient.Adresa = model.Adresa;
               

                _pacientRepository.Update(pacient);
                return RedirectToAction("index");
            }
            return View();
        }



        [Route("Edit/{id?}")]
        public ViewResult Edit(int id)
        {
            Pacient pacient = _pacientRepository.GetPacient(id);
            PacientiEditViewModel pacientiEditViewModel = new PacientiEditViewModel
            {
                Id = pacient.Id,
                Nume = pacient.Nume,
                Prenume = pacient.Prenume,
                CNP = pacient.CNP,
                Sexul = pacient.Sexul,
                Adresa = pacient.Adresa
            };
            return View(pacientiEditViewModel);
        }


        [HttpPost]
        public IActionResult Create(Pacient pacient)
        {
            if (ModelState.IsValid)
            {
         

                Pacient newpacient = new Pacient
                {
                    Nume = pacient.Nume,
                    Prenume = pacient.Prenume,
                    CNP = pacient.CNP,
                    Sexul = pacient.Sexul,
                    Adresa = pacient.Adresa
                };
                _pacientRepository.Add(newpacient);
                return RedirectToAction("details", new { id = newpacient.Id });
            }
            return View();
        }

    }
}
