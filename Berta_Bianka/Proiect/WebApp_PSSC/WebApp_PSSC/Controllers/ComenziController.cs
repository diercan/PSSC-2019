using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp_PSSC.Models;

namespace WebApp_PSSC.Controllers
{
    public class ComenziController : Controller
    {
        private WebApp_PSSCContext db = new WebApp_PSSCContext();

        // GET: Comenzi
        public ActionResult Index()
        {
            var comenzi = db.Comenzi.Include(c => c.Client).Include(c => c.Produs);
            return View(comenzi.ToList());
        }

        // GET: Comenzi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comanda comanda = db.Comenzi.Find(id);
            if (comanda == null)
            {
                return HttpNotFound();
            }
            return View(comanda);
        }

        // GET: Comenzi/Create
        public ActionResult Create()
        {
            ViewBag.IdClient = new SelectList(db.Clienti, "IdClient", "Nume");
            ViewBag.IdProdus = new SelectList(db.Produse, "IdProdus", "TipProdus");
            return View();
        }

        // POST: Comenzi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdComanda,IdClient,IdProdus,NrProduse")] Comanda comanda)
        {
            if (ModelState.IsValid)
            {
                db.Comenzi.Add(comanda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdClient = new SelectList(db.Clienti, "IdClient", "Nume", comanda.IdClient);
            ViewBag.IdProdus = new SelectList(db.Produse, "IdProdus", "TipProdus", comanda.IdProdus);
            return View(comanda);
        }

        // GET: Comenzi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comanda comanda = db.Comenzi.Find(id);
            if (comanda == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdClient = new SelectList(db.Clienti, "IdClient", "Nume", comanda.IdClient);
            ViewBag.IdProdus = new SelectList(db.Produse, "IdProdus", "TipProdus", comanda.IdProdus);
            return View(comanda);
        }

        // POST: Comenzi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdComanda,IdClient,IdProdus,NrProduse")] Comanda comanda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comanda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdClient = new SelectList(db.Clienti, "IdClient", "Nume", comanda.IdClient);
            ViewBag.IdProdus = new SelectList(db.Produse, "IdProdus", "TipProdus", comanda.IdProdus);
            return View(comanda);
        }

        // GET: Comenzi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comanda comanda = db.Comenzi.Find(id);
            if (comanda == null)
            {
                return HttpNotFound();
            }
            return View(comanda);
        }

        // POST: Comenzi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comanda comanda = db.Comenzi.Find(id);
            db.Comenzi.Remove(comanda);
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
