using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TeacherMVC.Models;

namespace TeacherMVC.Controllers
{
    public class FeedbackReceiveController : Controller
    {
        // GET: FeedbackReceive
        public ActionResult Index()
        {

            List<FeedbackReceive> list = new List<FeedbackReceive>();
            list.Add(new FeedbackReceive{ Id = Guid.NewGuid(), GoodFeedback = "first feedback" });

            return View(list);
        }

        // GET: FeedbackReceive/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FeedbackReceive/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: FeedbackReceive/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FeedbackReceive/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FeedbackReceive/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FeedbackReceive/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FeedbackReceive/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}