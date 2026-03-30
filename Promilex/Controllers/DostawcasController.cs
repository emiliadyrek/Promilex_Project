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
    public class DostawcasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DostawcasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Dostawcy.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dostawca = await _context.Dostawcy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dostawca == null)
            {
                return NotFound();
            }

            return View(dostawca);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NazwaFirmy,NumerTelefonu")] Dostawca dostawca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dostawca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dostawca);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dostawca = await _context.Dostawcy.FindAsync(id);
            if (dostawca == null)
            {
                return NotFound();
            }
            return View(dostawca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NazwaFirmy,NumerTelefonu")] Dostawca dostawca)
        {
            if (id != dostawca.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dostawca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DostawcaExists(dostawca.Id))
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
            return View(dostawca);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dostawca = await _context.Dostawcy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dostawca == null)
            {
                return NotFound();
            }

            return View(dostawca);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dostawca = await _context.Dostawcy.FindAsync(id);
            if (dostawca != null)
            {
                _context.Dostawcy.Remove(dostawca);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DostawcaExists(int id)
        {
            return _context.Dostawcy.Any(e => e.Id == id);
        }
    }
}
