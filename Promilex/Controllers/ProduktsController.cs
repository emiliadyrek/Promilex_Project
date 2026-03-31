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

        // GET: Produkts
        public async Task<IActionResult> Index()
        {
            // KLUCZOWA POPRAWKA: Dodano .Include(p => p.Skladniki)
            // Teraz zielona ikonka w widoku odczyta poprawną liczbę składników
            var produkty = _context.Produkty
                .Include(p => p.Kategoria)
                .Include(p => p.Producent)
                .Include(p => p.Skladniki);

            return View(await produkty.ToListAsync());
        }

        // GET: Produkts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var produkt = await _context.Produkty
                .Include(p => p.Kategoria)
                .Include(p => p.Producent)
                .Include(p => p.Skladniki)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (produkt == null) return NotFound();

            return View(produkt);
        }

        // GET: Produkts/Create
        public IActionResult Create()
        {
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa");
            ViewData["ProducentId"] = new SelectList(_context.Producenci, "Id", "Nazwa");
            ViewBag.SkladnikiId = new MultiSelectList(_context.Skladniki, "Id", "Nazwa");
            return View();
        }

        // POST: Produkts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Opis,Cena,ZawartoscAlkoholu,KategoriaId,ProducentId")] Produkt produkt, int[] wybraneSkladniki)
        {
            if (ModelState.IsValid)
            {
                // Inicjalizacja listy, jeśli jest nullem w modelu
                produkt.Skladniki = new List<Skladnik>();

                if (wybraneSkladniki != null)
                {
                    foreach (var sId in wybraneSkladniki)
                    {
                        var skladnik = await _context.Skladniki.FindAsync(sId);
                        if (skladnik != null) produkt.Skladniki.Add(skladnik);
                    }
                }

                _context.Add(produkt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", produkt.KategoriaId);
            ViewData["ProducentId"] = new SelectList(_context.Producenci, "Id", "Nazwa", produkt.ProducentId);
            ViewBag.SkladnikiId = new MultiSelectList(_context.Skladniki, "Id", "Nazwa", wybraneSkladniki);
            return View(produkt);
        }

        // GET: Produkts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var produkt = await _context.Produkty
                .Include(p => p.Skladniki)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (produkt == null) return NotFound();

            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", produkt.KategoriaId);
            ViewData["ProducentId"] = new SelectList(_context.Producenci, "Id", "Nazwa", produkt.ProducentId);

            var wybraneIds = produkt.Skladniki.Select(s => s.Id).ToArray();
            ViewBag.SkladnikiId = new MultiSelectList(_context.Skladniki, "Id", "Nazwa", wybraneIds);

            return View(produkt);
        }

        // POST: Produkts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Opis,Cena,ZawartoscAlkoholu,KategoriaId,ProducentId")] Produkt produkt, int[] wybraneSkladniki)
        {
            if (id != produkt.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var produktToUpdate = await _context.Produkty
                        .Include(p => p.Skladniki)
                        .FirstOrDefaultAsync(p => p.Id == id);

                    if (produktToUpdate == null) return NotFound();

                    // Mapowanie pól
                    _context.Entry(produktToUpdate).CurrentValues.SetValues(produkt);

                    // Aktualizacja Many-to-Many
                    produktToUpdate.Skladniki.Clear();
                    if (wybraneSkladniki != null)
                    {
                        foreach (var sId in wybraneSkladniki)
                        {
                            var skladnik = await _context.Skladniki.FindAsync(sId);
                            if (skladnik != null) produktToUpdate.Skladniki.Add(skladnik);
                        }
                    }

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

        // GET: Produkts/Delete/5
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

        // POST: Produkts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produkt = await _context.Produkty
                .Include(p => p.Skladniki) // Zapewnia poprawne usunięcie powiązań w tabeli łączącej
                .FirstOrDefaultAsync(p => p.Id == id);

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