using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Proiect_PSSC.Models;
using Proiect_PSSC.ViewModels;

namespace Proiect_PSSC.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {

        public readonly IPacientRepository _pacientRepository;
        private readonly MessageBroker _broker;

        public HomeController(IPacientRepository pacientRepository, MessageBroker broker)
        {
            _pacientRepository = pacientRepository;
            _broker = broker;
        }

        [Route("Index")]
        public IActionResult Index(string search = null)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var foundPacienti = _pacientRepository.SearchPacienti(search);
                return View(foundPacienti);
            }
            var pacienti = _pacientRepository.GetAllPacient();
            return View(pacienti);
        }


        [Route("")]
        [Route("~/")]
        [Route("Index1")]
        public IActionResult Index1(string search = null)
        {
                var foundPacienti = _pacientRepository.SearchPacienti(search);
                return View(foundPacienti);
        }

        [Route("Details/{id?}")]
        public async Task<ViewResult> Details(int id)
        {

            var pacient = _pacientRepository.GetPacient(id);

            var jsonPacient = JsonConvert.SerializeObject(pacient); 
            await _broker.SendMessage(jsonPacient, "WebToWorker");

            string coeficient = await _broker.ReceiveMessage("WorkerToWeb");

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Pacient = _pacientRepository.GetPacient(id),
                Coeficient = coeficient
            };

            return View(homeDetailsViewModel);
        }



        [Route("DetailsPacient/{id?}")]
        public async Task<ViewResult>  DetailsPacient(int id)
        {
            var pacient = _pacientRepository.GetPacient(id);
            var jsonPacient = JsonConvert.SerializeObject(pacient);

            await _broker.SendMessage(jsonPacient, "WebToWorker");

            string coeficient = await _broker.ReceiveMessage("WorkerToWeb");

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Pacient = _pacientRepository.GetPacient(id),
                Coeficient = coeficient
            };
            return View(homeDetailsViewModel);
        }


        [HttpPost("Update")]
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
                pacient.Inaltime = model.Inaltime;
                pacient.Greutate = model.Greutate;
                pacient.Temperatura = model.Temperatura;
                pacient.Puls = model.Puls;
                pacient.Tensiune = model.Tensiune;
                pacient.Frecventa_Cardiaca = model.Frecventa_Cardiaca;
                pacient.Frecv_Respiratorie = model.Frecv_Respiratorie;
                pacient.Inspectie_Toracica = model.Inspectie_Toracica;
                pacient.Auscultatie = model.Auscultatie;
                pacient.Hemoglobina = model.Hemoglobina;
                pacient.Leucocite = model.Leucocite;
                pacient.Eritrocite = model.Eritrocite;
                pacient.Trombocite = model.Trombocite;
                pacient.Hematocrit = model.Hematocrit;


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
                Adresa = pacient.Adresa,
                Inaltime = pacient.Inaltime,
                Greutate = pacient.Greutate,
                Temperatura = pacient.Temperatura,
                Puls = pacient.Puls,
                Tensiune = pacient.Tensiune,
                Frecventa_Cardiaca = pacient.Frecventa_Cardiaca,
                Frecv_Respiratorie = pacient.Frecv_Respiratorie,
                Inspectie_Toracica = pacient.Inspectie_Toracica,
                Auscultatie = pacient.Auscultatie,
                Hemoglobina = pacient.Hemoglobina,
                Leucocite = pacient.Leucocite,
                Eritrocite = pacient.Eritrocite,
                Trombocite = pacient.Trombocite,
                Hematocrit = pacient.Hematocrit

            };
            return View(pacientiEditViewModel);
        }

     
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatePacientiViewModel pacient)
        {
            if (ModelState.IsValid)
            {
         

                Pacient newpacient = new Pacient
                {
                    Nume = pacient.Nume,
                    Prenume = pacient.Prenume,
                    CNP = pacient.CNP,
                    Sexul = pacient.Sexul,
                    Adresa = pacient.Adresa,
                    Inaltime = pacient.Inaltime,
                    Greutate = pacient.Greutate,
                    Temperatura = pacient.Temperatura,
                    Puls = pacient.Puls,
                    Tensiune = pacient.Tensiune,
                    Frecventa_Cardiaca = pacient.Frecventa_Cardiaca,
                    Frecv_Respiratorie = pacient.Frecv_Respiratorie,
                    Inspectie_Toracica = pacient.Inspectie_Toracica,
                    Auscultatie = pacient.Auscultatie,
                    Hemoglobina = pacient.Hemoglobina,
                    Leucocite = pacient.Leucocite,
                    Eritrocite = pacient.Eritrocite,
                    Trombocite = pacient.Trombocite,
                    Hematocrit = pacient.Hematocrit

                };
                _pacientRepository.Add(newpacient);
                return RedirectToAction("index", new { id = newpacient.Id });
            }
            return View();
        }

      
        [Route("Delete/{id}")]
        public IActionResult Delete( int id)
        {
       
            Pacient pacient = _pacientRepository.Delete(id);
            return RedirectToAction("index");
            
        }
     
    }
}
