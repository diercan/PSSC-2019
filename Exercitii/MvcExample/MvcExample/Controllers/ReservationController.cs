using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcExample.Models;
using MvcExample.Repository;

namespace MvcExample.Controllers
{
	public class ReservationController : Controller
	{
		private readonly IReservationRepository reservationRepository;

		public ReservationController(IReservationRepository reservationRepository)
		{
			this.reservationRepository = reservationRepository;
		}

		// GET: Reservation
		public ActionResult Index()
		{
			return View(reservationRepository.GetAllReservations());
		}

		// GET: Reservation/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: Reservation/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Reservation/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Reservation reservation)
		{
			try
			{
				reservation.Id = Guid.NewGuid();
				reservationRepository.CreateReservation(reservation);

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: Reservation/Edit/5
		public ActionResult Edit(int id)
		{

			return View();
		}

		// POST: Reservation/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Reservation reservation)
		{
			try
			{
                // TODO: Add update logic here
                reservationRepository.EditReservation(reservation);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
                //return View();
                return RedirectToAction(nameof(Index));
            }
		}

		// GET: Reservation/Delete/5
		public ActionResult Delete(Guid id)
		{
			var reservation = reservationRepository.GetReservationById(id);
			return View(reservation);
		}

		// POST: Reservation/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(Guid id, IFormCollection collection)
		{
			try
			{
				var reservation = reservationRepository.GetReservationById(id);
				reservationRepository.DeleteReservation(reservation);

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}