using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedFind.Interfaces;
using MedFind.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedFind.Controllers
{
    public class StudentController : Controller
    {
        private IStudent _student;
        //private readonly SignInManager<IdentityUser> signInManager;

        public  StudentController(IStudent student)
        {
            _student = student;
            
        }
    /*
        public ActionResult LoginStudent()
        {
                    return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginStudent(Student student)
        {
            try
            {
                var model = _student.CheckStudent(student);
                if (model !=null)
                {
                    return View("~/Views/Student/Details.cshtml",model);
                }
                else
                    return View();
            }
            catch 
            {

                return View();
            }
        }*/

    // GET: Student
        public ActionResult Index()
        {
            var model = _student.GetAllStudents();
            return base.View(model);
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            //return View("~/Views/Student/Details.cshtml", model);
            return View();
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
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

        // GET: Student/Edit/5
        public ActionResult Edit(string studentAccount)
        {
            return View("~/Views/Student/Edit.cshtml");
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
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

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Delete/5
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