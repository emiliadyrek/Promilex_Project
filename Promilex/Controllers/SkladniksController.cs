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

        // GET: Skladniks
        // To zasila zielone licznik w "Bazie Receptur"
        public async Task<IActionResult> Index()
        {
            var skladniki = await _context.Skladniki
                .Include(s => s.Produkty) // Kluczowe dla licznika @item.Produkty.Count()
                .AsNoTracking()
                .ToListAsync();
            return View(skladniki);
        }

        // GET: Skladniks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var skladnik = await _context.Skladniki
                .Include(s => s.Produkty) // Pozwala wyświetlić listę trunków w Details
                .FirstOrDefaultAsync(m => m.Id == id);

            if (skladnik == null) return NotFound();

            return View(skladnik);
        }

        // GET: Skladniks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skladniks/Create
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

        // GET: Skladniks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var skladnik = await _context.Skladniki.FindAsync(id);
            if (skladnik == null) return NotFound();

            return View(skladnik);
        }

        // POST: Skladniks/Edit/5
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

        // GET: Skladniks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var skladnik = await _context.Skladniki
                .Include(s => s.Produkty) // Dzięki temu widok Delete wie, czy składnik jest używany
                .FirstOrDefaultAsync(m => m.Id == id);

            if (skladnik == null) return NotFound();

            return View(skladnik);
        }

        // POST: Skladniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skladnik = await _context.Skladniki
                .Include(s => s.Produkty) // Ważne: EF usunie też wpisy w tabeli łączącej
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