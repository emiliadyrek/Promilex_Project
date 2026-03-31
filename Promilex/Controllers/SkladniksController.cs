using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Promiex.Models;

namespace Promilex.Controllers
{
    public class SkladniksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SkladniksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var skladniki = await _context.Skladniki
                .Include(s => s.Produkty) 
                .AsNoTracking()
                .ToListAsync();
            return View(skladniki);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var skladnik = await _context.Skladniki
                .Include(s => s.Produkty) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (skladnik == null) return NotFound();

            return View(skladnik);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa")] Skladnik skladnik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skladnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skladnik);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var skladnik = await _context.Skladniki.FindAsync(id);
            if (skladnik == null) return NotFound();

            return View(skladnik);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa")] Skladnik skladnik)
        {
            if (id != skladnik.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skladnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkladnikExists(skladnik.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(skladnik);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var skladnik = await _context.Skladniki
                .Include(s => s.Produkty) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (skladnik == null) return NotFound();

            return View(skladnik);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skladnik = await _context.Skladniki
                .Include(s => s.Produkty) 
                .FirstOrDefaultAsync(s => s.Id == id);

            if (skladnik != null)
            {
                _context.Skladniki.Remove(skladnik);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool SkladnikExists(int id)
        {
            return _context.Skladniki.Any(e => e.Id == id);
        }
    }
}