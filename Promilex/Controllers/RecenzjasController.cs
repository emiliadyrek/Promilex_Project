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
    public class RecenzjasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecenzjasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            var recenzje = _context.Recenzje.Include(r => r.Produkt);
            return View(await recenzje.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var recenzja = await _context.Recenzje
                .Include(r => r.Produkt)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recenzja == null) return NotFound();

            return View(recenzja);
        }

        public IActionResult Create()
        {
            ViewData["ProduktId"] = new SelectList(_context.Produkty, "Id", "Nazwa");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProduktId,Tresc,Ocena")] Recenzja recenzja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recenzja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProduktId"] = new SelectList(_context.Produkty, "Id", "Nazwa", recenzja.ProduktId);
            return View(recenzja);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var recenzja = await _context.Recenzje.FindAsync(id);
            if (recenzja == null) return NotFound();

            ViewData["ProduktId"] = new SelectList(_context.Produkty, "Id", "Nazwa", recenzja.ProduktId);
            return View(recenzja);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProduktId,Tresc,Ocena")] Recenzja recenzja)
        {
            if (id != recenzja.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recenzja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecenzjaExists(recenzja.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProduktId"] = new SelectList(_context.Produkty, "Id", "Nazwa", recenzja.ProduktId);
            return View(recenzja);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var recenzja = await _context.Recenzje
                .Include(r => r.Produkt)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recenzja == null) return NotFound();

            return View(recenzja);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recenzja = await _context.Recenzje.FindAsync(id);
            if (recenzja != null)
            {
                _context.Recenzje.Remove(recenzja);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RecenzjaExists(int id)
        {
            return _context.Recenzje.Any(e => e.Id == id);
        }
    }
}