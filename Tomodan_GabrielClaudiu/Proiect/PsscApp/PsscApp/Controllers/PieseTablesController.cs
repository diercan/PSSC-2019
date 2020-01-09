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
    public class PieseTablesController : Controller
    {
        private PieseDataBaseEntities db = new PieseDataBaseEntities();

        // GET: PieseTables
        public ActionResult Index()
        {
            return View(db.PieseTables.ToList());
        }

        // GET: PieseTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PieseTable pieseTable = db.PieseTables.Find(id);
            if (pieseTable == null)
            {
                return HttpNotFound();
            }
            return View(pieseTable);
        }

        // GET: PieseTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PieseTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PieseID,Tip_piesa,Marca,Pret,Descriere")] PieseTable pieseTable)
        {
            if (ModelState.IsValid)
            {
                db.PieseTables.Add(pieseTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pieseTable);
        }

        // GET: PieseTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PieseTable pieseTable = db.PieseTables.Find(id);
            if (pieseTable == null)
            {
                return HttpNotFound();
            }
            return View(pieseTable);
        }

        // POST: PieseTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PieseID,Tip_piesa,Marca,Pret,Descriere")] PieseTable pieseTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pieseTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pieseTable);
        }

        // GET: PieseTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PieseTable pieseTable = db.PieseTables.Find(id);
            if (pieseTable == null)
            {
                return HttpNotFound();
            }
            return View(pieseTable);
        }

        // POST: PieseTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PieseTable pieseTable = db.PieseTables.Find(id);
            db.PieseTables.Remove(pieseTable);
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
