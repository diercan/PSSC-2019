using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkinShopCSGO.Models;

namespace SkinShopCSGO.Controllers
{
    public class SkinsController : Controller
    {
        private SkinShopCSGOContext db = new SkinShopCSGOContext();

        // GET: Skins
        public ActionResult Index()
        {
            return View(db.Skins.ToList());
        }

        // GET: Skins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skin skin = db.Skins.Find(id);
            if (skin == null)
            {
                return HttpNotFound();
            }
            return View(skin);
        }

        // GET: Skins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Skins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SkinId,Name,Condition,Collection")] Skin skin)
        {
            if (ModelState.IsValid)
            {
                db.Skins.Add(skin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skin);
        }

        // GET: Skins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skin skin = db.Skins.Find(id);
            if (skin == null)
            {
                return HttpNotFound();
            }
            return View(skin);
        }

        // POST: Skins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SkinId,Name,Condition,Collection")] Skin skin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skin);
        }

        // GET: Skins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skin skin = db.Skins.Find(id);
            if (skin == null)
            {
                return HttpNotFound();
            }
            return View(skin);
        }

        // POST: Skins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Skin skin = db.Skins.Find(id);
            db.Skins.Remove(skin);
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
