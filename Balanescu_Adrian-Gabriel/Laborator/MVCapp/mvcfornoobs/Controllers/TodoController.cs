using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvcfornoobs.Models;

namespace mvcfornoobs.Controllers
{
    public class TodoController : Controller
    {
        static TodoList todolist = new TodoList();
        public TodoController()
        {
            // todolist.listOfEntries.Add(new TodoEntry("A todo", "this todo is important", DateTime.Now));
            // todolist.listOfEntries.Add(new TodoEntry("Another todo", "this is a secret todo", DateTime.Now));
        }
        public IActionResult Todo()
        {
            return View(todolist.listOfEntries);
        }

        [HttpPost]
        public ActionResult TodoAdd(string titlu, string mesaj, string __RequestVerificationToken)
        {
            string Titlu = titlu;
            string Mesaj = mesaj;
            
            todolist.listOfEntries.Add(new TodoEntry(Titlu, Mesaj, DateTime.Now));
            

            return View("Todo", todolist.listOfEntries);
        }
    }
}