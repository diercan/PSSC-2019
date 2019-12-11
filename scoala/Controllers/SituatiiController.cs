using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using scoala.Models;

namespace scoala.Controllers
{
    public class SituatiiController : Controller
    {
        private ScoalaContext db = new ScoalaContext();

        // GET: Situatii
        public ActionResult Index()
        {
            var situatii = db.Situatii.Include(s => s.Elev);
            return View(situatii.ToList());
        }

        // GET: Situatii/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Situatie situatie = db.Situatii.Find(id);
            if (situatie == null)
            {
                return HttpNotFound();
            }
            return View(situatie);
        }

        // GET: Situatii/Create
        public ActionResult Create()
        {
            ViewBag.IDElev = new SelectList(db.Elevi, "IDElev", "Nume");
            return View();
        }

        // POST: Situatii/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSituatie,IDElev,Data,Materie,Nota,SituatieOra")] Situatie situatie)
        {
            if (ModelState.IsValid)
            {
                db.Situatii.Add(situatie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDElev = new SelectList(db.Elevi, "IDElev", "Nume", situatie.IDElev);
            return View(situatie);
        }

        // GET: Situatii/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Situatie situatie = db.Situatii.Find(id);
            if (situatie == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDElev = new SelectList(db.Elevi, "IDElev", "Nume", situatie.IDElev);
            return View(situatie);
        }

        // POST: Situatii/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSituatie,IDElev,Data,Materie,Nota,SituatieOra")] Situatie situatie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(situatie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDElev = new SelectList(db.Elevi, "IDElev", "Nume", situatie.IDElev);
            return View(situatie);
        }

        // GET: Situatii/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Situatie situatie = db.Situatii.Find(id);
            if (situatie == null)
            {
                return HttpNotFound();
            }
            return View(situatie);
        }

        // POST: Situatii/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Situatie situatie = db.Situatii.Find(id);
            db.Situatii.Remove(situatie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
