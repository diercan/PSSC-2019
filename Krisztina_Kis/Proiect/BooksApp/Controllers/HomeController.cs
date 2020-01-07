using BooksApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksApp.Controllers
{
    public class HomeController : Controller
    {
        private BooksDBEntities _db = new BooksDBEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View(_db.Books.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude ="Id")]Book bookToCreate)
        {
            if (!ModelState.IsValid)
                return View();

            _db.Books.Add(bookToCreate);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            var bookToEdit = (from m in _db.Books
                              where m.Id == id
                              select m).First();
            return View(bookToEdit);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Book bookToEdit)
        {
            var originalBook = (from m in _db.Books
                                where m.Id == bookToEdit.Id
                                select m).First();

            if (!ModelState.IsValid)
                return View(originalBook);

            _db.Entry(originalBook).CurrentValues.SetValues(bookToEdit);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
