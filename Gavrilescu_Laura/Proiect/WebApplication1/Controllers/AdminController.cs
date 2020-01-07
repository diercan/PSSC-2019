using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProgramareRepository programareRepository;
        public AdminController(IProgramareRepository programareRepository)
        {
            this.programareRepository = programareRepository;
        }
        public IActionResult Index()
        {
            return View(programareRepository.GetAllReservations());
        }

        public ActionResult Edit(Guid id)
        {
            return View(programareRepository.GetReservationById(id));
        }
        // POST: Reservation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
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
        public ActionResult Delete(Guid id)
        {
            var reservation = programareRepository.GetReservationById(id);
            return View(reservation);
        }

        // POST: Reservation/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var reservation = programareRepository.GetReservationById(id);
                programareRepository.DeleteReservation(reservation);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}