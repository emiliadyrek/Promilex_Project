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
    public class ProduktsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProduktsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            var produkty = _context.Produkty
                .Include(p => p.Kategoria)
                .Include(p => p.Producent);

            return View(await produkty.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var produkt = await _context.Produkty
                .Include(p => p.Kategoria)
                .Include(p => p.Producent)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (produkt == null) return NotFound();

            return View(produkt);
        }

        public IActionResult Create()
        {
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa");
            ViewData["ProducentId"] = new SelectList(_context.Producenci, "Id", "Nazwa");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Opis,Cena,ZawartoscAlkoholu,KategoriaId,ProducentId")] Produkt produkt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produkt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", produkt.KategoriaId);
            ViewData["ProducentId"] = new SelectList(_context.Producenci, "Id", "Nazwa", produkt.ProducentId);
            return View(produkt);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var produkt = await _context.Produkty.FindAsync(id);
            if (produkt == null) return NotFound();

            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", produkt.KategoriaId);
            ViewData["ProducentId"] = new SelectList(_context.Producenci, "Id", "Nazwa", produkt.ProducentId);
            return View(produkt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Opis,Cena,ZawartoscAlkoholu,KategoriaId,ProducentId")] Produkt produkt)
        {
            if (id != produkt.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produkt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduktExists(produkt.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", produkt.KategoriaId);
            ViewData["ProducentId"] = new SelectList(_context.Producenci, "Id", "Nazwa", produkt.ProducentId);
            return View(produkt);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var produkt = await _context.Produkty
                .Include(p => p.Kategoria)
                .Include(p => p.Producent)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (produkt == null) return NotFound();

            return View(produkt);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produkt = await _context.Produkty.FindAsync(id);
            if (produkt != null)
            {
                _context.Produkty.Remove(produkt);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProduktExists(int id)
        {
            return _context.Produkty.Any(e => e.Id == id);
        }
    }
}