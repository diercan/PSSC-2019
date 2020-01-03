using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeTest.Data;
using WeTest.Models;

namespace WeTest.Controllers
{
    public class TestersController : Controller
    {
        private readonly WeTestContext _context;

        public TestersController(WeTestContext context)
        {
            _context = context;
        }

        // GET: Testers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tester.ToListAsync());
        }

        // GET: Testers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tester = await _context.Tester
                .FirstOrDefaultAsync(m => m.TesterId == id);
            if (tester == null)
            {
                return NotFound();
            }

            return View(tester);
        }

        // GET: Testers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Testers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TesterId,TesterName")] Tester tester)
        {
            
            if (ModelState.IsValid)
            {
                if (TesterExists(tester.TesterId)) {
                    return Content("Tester Id already exists.Please use company id number.");
                }
                _context.Add(tester);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tester);
        }

        // GET: Testers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tester = await _context.Tester.FindAsync(id);
            if (tester == null)
            {
                return NotFound();
            }
            return View(tester);
        }

        // POST: Testers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TesterId,TesterName")] Tester tester)
        {
            if (id != tester.TesterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tester);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TesterExists(tester.TesterId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tester);
        }

        // GET: Testers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tester = await _context.Tester
                .FirstOrDefaultAsync(m => m.TesterId == id);
            if (tester == null)
            {
                return NotFound();
            }

            return View(tester);
        }

        // POST: Testers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tester = await _context.Tester.FindAsync(id);
            _context.Tester.Remove(tester);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TesterExists(string id)
        {
            return _context.Tester.Any(e => e.TesterId == id);
        }
    }
}
