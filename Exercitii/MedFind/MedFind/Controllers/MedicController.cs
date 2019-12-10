using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedFind.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedFind.Controllers
{
    public class MedicController : Controller
    {

        private IMedic _medic;
        //private readonly SignInManager<IdentityUser> signInManager;

        public MedicController(IMedic medic)
        {
            _medic = medic;

        }
        // GET: Medic
        public ActionResult Index()
        {
            return View();
        }

        // GET: Medic/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Medic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medic/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Medic/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Medic/Edit/5
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

        // GET: Medic/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Medic/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}