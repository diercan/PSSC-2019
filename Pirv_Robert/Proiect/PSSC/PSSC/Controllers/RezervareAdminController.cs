using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PSSC.Repository;
using PSSC.Models;

namespace PSSC.Controllers
{
    public class RezervareAdminController : Controller
    {
        private readonly IRezervareRepository rezervareRepository;

        public RezervareAdminController(IRezervareRepository rezervareRepository)
        {
            this.rezervareRepository = rezervareRepository;
        }
        // GET: RezervareAdmin
  
        public ActionResult Index()
        {
            return View(rezervareRepository.ObtineRezervari());
        }

        // GET: RezervareAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RezervareAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RezervareAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rezervare rezervare)
        {
            try
            {
                rezervare.IdUnic = Guid.NewGuid();
                rezervareRepository.CreareRezervare(rezervare);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RezervareAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RezervareAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RezervareAdmin/Delete/5
        public ActionResult Delete(Guid id)
        {
            var rezervare = rezervareRepository.ObtineRezervareDupaGuid(id);
            return View(rezervare);
        }

        // POST: RezervareAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                var rezervare = rezervareRepository.ObtineRezervareDupaGuid(id);
                rezervareRepository.StergeRezervare(rezervare);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
