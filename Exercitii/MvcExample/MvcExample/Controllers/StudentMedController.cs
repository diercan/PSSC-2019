using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MvcExample.Controllers
{
    public class StudentMedController : Controller
    {
        // GET: StudentMed
        public ActionResult Index()
        {
            return View();
        }

        // GET: StudentMed/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentMed/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentMed/Create
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

        // GET: StudentMed/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentMed/Edit/5
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

        // GET: StudentMed/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentMed/Delete/5
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