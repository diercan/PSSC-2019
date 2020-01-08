using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakeAuction.Models;

namespace TakeAuction.Controllers
{
    public class SignInController : Controller
    {
        Connection conn = new Connection();
        // GET: SignIn
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost] //preia din textbox si duce in Controllers
        public ActionResult Account( SignIn account ) // account - match intre user si pass (admin si useri)
        {             
            ModelState.Clear(); //refresh
            TempData["data"] = account;
            if (account.User == "Andrei" && account.Password == "andreipass")
            {
                return RedirectToAction("Index","Admin", account);
            }
            else 
            {
                for(int i=0; i < conn.GetSqlRowsUsers().Count; i++)
                {
                    if(conn.GetSqlRowsUsers()[i].User == account.User && conn.GetSqlRowsUsers()[i].Password == account.Password)
                    {
                        return RedirectToAction("Index", "User");
                    }
                }
            }

            return View("Index");
        }

        // GET: SignIn/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SignIn/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SignIn/Create
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

        // GET: SignIn/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SignIn/Edit/5
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

        // GET: SignIn/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SignIn/Delete/5
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
