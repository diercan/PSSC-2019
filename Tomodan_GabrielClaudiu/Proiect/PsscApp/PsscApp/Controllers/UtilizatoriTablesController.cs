using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PsscApp.Models;

namespace PsscApp.Controllers
{
    public class UtilizatoriTablesController : Controller
    {
        private PieseDataBaseEntities db = new PieseDataBaseEntities();

        // GET: UtilizatoriTables
        public ActionResult Index()
        {
            return View(db.UtilizatoriTables.ToList());
        }

        // GET: UtilizatoriTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UtilizatoriTable utilizatoriTable = db.UtilizatoriTables.Find(id);
            if (utilizatoriTable == null)
            {
                return HttpNotFound();
            }
            return View(utilizatoriTable);
        }

        // GET: UtilizatoriTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UtilizatoriTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,Nume_utilizator,Parola,email")] UtilizatoriTable utilizatoriTable)
        {
            if (ModelState.IsValid)
            {
                db.UtilizatoriTables.Add(utilizatoriTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(utilizatoriTable);
        }

        // GET: UtilizatoriTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UtilizatoriTable utilizatoriTable = db.UtilizatoriTables.Find(id);
            if (utilizatoriTable == null)
            {
                return HttpNotFound();
            }
            return View(utilizatoriTable);
        }

        // POST: UtilizatoriTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,Nume_utilizator,Parola,email")] UtilizatoriTable utilizatoriTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilizatoriTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utilizatoriTable);
        }

        // GET: UtilizatoriTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UtilizatoriTable utilizatoriTable = db.UtilizatoriTables.Find(id);
            if (utilizatoriTable == null)
            {
                return HttpNotFound();
            }
            return View(utilizatoriTable);
        }

        // POST: UtilizatoriTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UtilizatoriTable utilizatoriTable = db.UtilizatoriTables.Find(id);
            db.UtilizatoriTables.Remove(utilizatoriTable);
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
