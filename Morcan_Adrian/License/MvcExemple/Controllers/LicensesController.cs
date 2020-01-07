using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LicenseWeb.Models;
using LicenseWeb.Services;

namespace LicenseWeb.Controllers
{
	public class LicensesController : Controller
	{
		private readonly ILicenseService reservationService;

		public LicensesController(ILicenseService reservationService)
		{
			this.reservationService = reservationService;
		}

		// GET: Licenses
		public ActionResult Index()
		{
			return View(reservationService.GetAllReservations());
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
		public ActionResult Create(Licenses reservation)
		{
			try
			{
				reservationService.CreateLicenses(reservation);

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

		// GET: Reservation/Delete/5
		public ActionResult Delete(Guid id)
		{
			var reservation = reservationService.GetLicenseById(id);
			return View(reservation);
		}

		// POST: Reservation/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(Guid id, IFormCollection collection)
		{
			try
			{
				reservationService.DeleteLicense(id);

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}