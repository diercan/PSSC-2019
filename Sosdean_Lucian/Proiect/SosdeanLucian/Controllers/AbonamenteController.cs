using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SosdeanLucian.Models;

namespace SosdeanLucian.Controllers
{
    public class AbonamenteController : Controller
    {
        private GymContext db = new GymContext();

        // GET: Abonamente
        public ActionResult Index()
        {
            var abonamente = db.Abonamente.Include(a => a.Client).Include(a => a.Oferta);
            return View(abonamente.ToList());
        }

        // GET: Abonamente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abonament abonament = db.Abonamente.Find(id);
            if (abonament == null)
            {
                return HttpNotFound();
            }
            return View(abonament);
        }

        // GET: Abonamente/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clienti, "ClientID", "NumeClient");
            ViewBag.OfertaID = new SelectList(db.Oferte, "OfertaID", "NumeOferta");
            return View();
        }

        // POST: Abonamente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AbonamentID,OfertaID,ClientID")] Abonament abonament)
        {
            if (ModelState.IsValid)
            {
                db.Abonamente.Add(abonament);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.Clienti, "ClientID", "NumeClient", abonament.ClientID);
            ViewBag.OfertaID = new SelectList(db.Oferte, "OfertaID", "NumeOferta", abonament.OfertaID);
            return View(abonament);
        }

        // GET: Abonamente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abonament abonament = db.Abonamente.Find(id);
            if (abonament == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clienti, "ClientID", "NumeClient", abonament.ClientID);
            ViewBag.OfertaID = new SelectList(db.Oferte, "OfertaID", "NumeOferta", abonament.OfertaID);
            return View(abonament);
        }

        // POST: Abonamente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AbonamentID,OfertaID,ClientID")] Abonament abonament)
        {
            if (ModelState.IsValid)
            {
                db.Entry(abonament).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clienti, "ClientID", "NumeClient", abonament.ClientID);
            ViewBag.OfertaID = new SelectList(db.Oferte, "OfertaID", "NumeOferta", abonament.OfertaID);
            return View(abonament);
        }

        // GET: Abonamente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abonament abonament = db.Abonamente.Find(id);
            if (abonament == null)
            {
                return HttpNotFound();
            }
            return View(abonament);
        }

        // POST: Abonamente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Abonament abonament = db.Abonamente.Find(id);
            db.Abonamente.Remove(abonament);
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
