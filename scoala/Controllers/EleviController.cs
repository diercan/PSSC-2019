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
    public class EleviController : Controller
    {
        private ScoalaContext db = new ScoalaContext();

        // GET: Elevi
        public ActionResult Index()
        {
            return View(db.Elevi.ToList());
        }

        // GET: Elevi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elev elev = db.Elevi.Find(id);
            if (elev == null)
            {
                return HttpNotFound();
            }
            return View(elev);
        }

        // GET: Elevi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Elevi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDElev,Nume,Prenume,Clasa")] Elev elev)
        {
            if (ModelState.IsValid)
            {
                db.Elevi.Add(elev);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(elev);
        }

        // GET: Elevi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elev elev = db.Elevi.Find(id);
            if (elev == null)
            {
                return HttpNotFound();
            }
            return View(elev);
        }

        // POST: Elevi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDElev,Nume,Prenume,Clasa")] Elev elev)
        {
            if (ModelState.IsValid)
            {
                db.Entry(elev).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(elev);
        }

        // GET: Elevi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elev elev = db.Elevi.Find(id);
            if (elev == null)
            {
                return HttpNotFound();
            }
            return View(elev);
        }

        // POST: Elevi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Elev elev = db.Elevi.Find(id);
            db.Elevi.Remove(elev);
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
