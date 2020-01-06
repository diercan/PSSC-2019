using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PSSC.Repository;
using PSSC.Models;

namespace PSSC.Controllers
{
    public class RezervareController : Controller
    {
        private readonly IRezervareRepository rezervareRepository;
        public RezervareController(IRezervareRepository rezervareRepository)
        {
            this.rezervareRepository = rezervareRepository;
        }

        // GET: Rezervare
        public ActionResult Index()
        {
            return View(rezervareRepository.ObtineRezervari());
        }

        // GET: Rezervare/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rezervare/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rezervare/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rezervare rezervare)
        {
            try
            {
                rezervare.IdUnic = Guid.NewGuid();
                rezervareRepository.CreareRezervare(rezervare);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Rezervare/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rezervare/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Rezervare/Delete/5
        public ActionResult Delete(Guid id)
        {
            var rezervare = rezervareRepository.ObtineRezervareDupaGuid(id);
            return View(rezervare);
        }

        // POST: Rezervare/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var rezervare = rezervareRepository.ObtineRezervareDupaGuid(id);
                rezervareRepository.StergeRezervare(rezervare);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}