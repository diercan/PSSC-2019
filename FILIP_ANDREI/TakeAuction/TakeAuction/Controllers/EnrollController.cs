using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakeAuction.Models;

namespace TakeAuction.Controllers
{
    public class EnrollController : Controller
    {
        Connection conn = new Connection();
        // GET: Enroll
        public ActionResult Index()
        {
            return View();
        }

        // GET: Enroll/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Enroll/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Enroll/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost] //preia din textbox si duce in Controllers
        public ActionResult CreateAccount(Enroll account) // account - match intre user si pass (admin si useri)
        {
            ModelState.Clear();
            if (account.Password == account.RetypePassword)
            {
                ViewBag.Text = conn.InsertUser(account);
                return View("SuccessOrFail");
            }
            else
                return View("Index");
        }

        // GET: Enroll/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Enroll/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Enroll/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Enroll/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
