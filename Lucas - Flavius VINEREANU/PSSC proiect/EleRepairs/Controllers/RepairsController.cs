using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EleRepairs.Data;
using EleRepairs.Models;

namespace EleRepairs.Controllers
{
    public class RepairsController : Controller
    {
        private readonly EleRepairsContext _context;

        public RepairsController(EleRepairsContext context)
        {
            _context = context;
        }

        // GET: Repairs
        public async Task<IActionResult> Index(string RepairsGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Repairs
                                    orderby m.Genre
                                    select m.Genre;

            var Repairs = from m in _context.Repairs
                 select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                Repairs = Repairs.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(RepairsGenre))
            {
                Repairs = Repairs.Where(x => x.Genre == RepairsGenre);
            }

            var RepairsGenreVM = new RepairsGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Repairs = await Repairs.ToListAsync()
            };

            return View(RepairsGenreVM);
        }

        // GET: Repairs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairs = await _context.Repairs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repairs == null)
            {
                return NotFound();
            }

            return View(repairs);
        }

        // GET: Repairs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Repairs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReceiveDate,ReleaseDate,Genre,Price,Rating")] Repairs repairs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(repairs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(repairs);
        }

        // GET: Repairs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairs = await _context.Repairs.FindAsync(id);
            if (repairs == null)
            {
                return NotFound();
            }
            return View(repairs);
        }

        // POST: Repairs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReceiveDate,ReleaseDate,Genre,Price,Rating")] Repairs repairs)
        {
            if (id != repairs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repairs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepairsExists(repairs.Id))
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
            return View(repairs);
        }

        // GET: Repairs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairs = await _context.Repairs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repairs == null)
            {
                return NotFound();
            }

            return View(repairs);
        }

        // POST: Repairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repairs = await _context.Repairs.FindAsync(id);
            _context.Repairs.Remove(repairs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepairsExists(int id)
        {
            return _context.Repairs.Any(e => e.Id == id);
        }
    }
}
