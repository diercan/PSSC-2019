using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPlanner.Data;
using MyPlanner.Models;
using MyPlanner.Repository;

namespace MyPlanner.Controllers
{
    public class MyTasksController : Controller
    {
        private readonly MyPlannerContext _context;
        public MyTasksController(MyPlannerContext context)
        {
            _context = context;
            
        }
       
        // GET: MyTasks
        public async Task<IActionResult> Index(string myTaskAsignee, string searchString)
        {
            if (UsersController.logged_user.username == "None")
            {
                return RedirectToAction("Privacy", "Home");
            }
            
            IQueryable<string> asigneeQuery = from m in _context.MyTask
                                            orderby m.Asignee
                                            select m.Asignee;

            var myTasks = from m in _context.MyTask select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                myTasks = myTasks.Where(s => s.Project.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(myTaskAsignee))
            {
                myTasks = myTasks.Where(x => x.Asignee == myTaskAsignee);
            }

            var myTaskAsigneeVM = new MyTaskAsigneeViewModel
            {
                Asignees = new SelectList(await asigneeQuery.Distinct().ToListAsync()),
                MyTasks = await myTasks.ToListAsync()
            };

            return View(myTaskAsigneeVM);
        }

        // GET: MyTasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myTask = await _context.MyTask
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myTask == null)
            {
                return NotFound();
            }

            return View(myTask);
        }

        // GET: MyTasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Due_Date,Project,Owner,Asignee,Status,Review")] MyTask myTask)
        {
            if (ModelState.IsValid)
            {
                myTask.Id = Guid.NewGuid();
                _context.Add(myTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(myTask);
        }

        // GET: MyTasks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myTask = await _context.MyTask.FindAsync(id);
            if (myTask == null)
            {
                return NotFound();
            }
            return View(myTask);
        }

        // POST: MyTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Description,Due_Date,Project,Owner,Asignee,Status,Review")] MyTask myTask)
        {
            if (id != myTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyTaskExists(myTask.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction("Dashboard", "Users", UsersController.logged_user);
                //return RedirectToAction("Index");
                ViewBag.Message = string.Format("Your changes have been saved");
                
            }
            return View(myTask);
        }

        // GET: MyTasks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myTask = await _context.MyTask
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myTask == null)
            {
                return NotFound();
            }

            return View(myTask);
        }

        // POST: MyTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var myTask = await _context.MyTask.FindAsync(id);
            _context.MyTask.Remove(myTask);
            await _context.SaveChangesAsync();
            ViewBag.Message = string.Format("Your changes have been saved");
            return View(myTask);
            //return RedirectToAction(nameof(Index));
        }

        private bool MyTaskExists(Guid id)
        {
            return _context.MyTask.Any(e => e.Id == id);
        }
    }
}
