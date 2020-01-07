using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Farmacie.Models;
using Microsoft.AspNetCore.Http;

namespace Farmacie.Controllers
{
    public class FarmacistsController : Controller
    {
        public readonly FarmacistDbContext _context;

        public FarmacistsController(FarmacistDbContext context)
        {
            _context = context;
        }

        // GET: Farmacists
        public async Task<IActionResult> Index()
        {
            return View(await _context.farmacisti.ToListAsync());
        }

        // GET: Farmacists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmacist = await _context.farmacisti
                .FirstOrDefaultAsync(m => m.ID == id);
            if (farmacist == null)
            {
                return NotFound();
            }

            return View(farmacist);
        }

        // GET: Farmacists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Farmacists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,strNume,strPrenume,strCNP,strNrTelefon,strEmail,strAdresaFarmacie,strParola")] Farmacist farmacist)
        {
            if (ModelState.IsValid)
            {
                Farmacist f = new Farmacist();
                f.CreateValueObject(farmacist);
                f.CreateDBObject();

                _context.Add(f);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(farmacist);
        }

        // GET: Farmacists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmacist = await _context.farmacisti.FindAsync(id);
            if (farmacist == null)
            {
                return NotFound();
            }
            return View(farmacist);
        }

        // POST: Farmacists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,strNume,strPrenume,strCNP,strNrTelefon,strEmail,strAdresaFarmacie,strParola")] Farmacist farmacist)
        {
            if (id != farmacist.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farmacist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmacistExists(farmacist.ID))
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
            return View(farmacist);
        }

        // GET: Farmacists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmacist = await _context.farmacisti
                .FirstOrDefaultAsync(m => m.ID == id);
            if (farmacist == null)
            {
                return NotFound();
            }

            return View(farmacist);
        }

        // POST: Farmacists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var farmacist = await _context.farmacisti.FindAsync(id);
            _context.farmacisti.Remove(farmacist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FarmacistExists(int id)
        {
            return _context.farmacisti.Any(e => e.ID == id);
        }
        public IActionResult Login()
        {
            return View();
        }


       
        public ActionResult Validate(Farmacist farmacist)
        {
            var _admin = _context.farmacisti.Where(s => s.strEmail == farmacist.strEmail);
            if (_admin!=null)
            {
                if (_admin != null)
                {

                    return Json(new { status = true, message = "Login Successfull!" });
                }
                else
                {
                    return Json(new { status = false, message = "Invalid Password!" });
                }
            }
            else
            {
                return Json(new { status = false, message = "Invalid Email!" });
            }
        }
        public async Task<IActionResult> Cautare(string searchString)
        {
            var farm = from m in _context.farmacisti
                       select m;
            @ViewBag.flag = 0;
            if (!String.IsNullOrEmpty(searchString))
            {
                farm = farm.Where(s => s.strNume.Contains(searchString));
                @ViewBag.flag = 1;
            }

            return View(await farm.ToListAsync());
        }

    }
}
