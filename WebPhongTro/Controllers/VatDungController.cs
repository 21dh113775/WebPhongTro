using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPhongTro.Models;

namespace WebPhongTro.Controllers
{
    public class VatDungController : Controller
    {
        private readonly PhongTroMVCContext _context;

        public VatDungController(PhongTroMVCContext context)
        {
            _context = context;
        }

        // GET: VatDung
        public async Task<IActionResult> Index()
        {
            var phongTroMVCContext = _context.VatDungs.Include(v => v.IdPhongNavigation);
            return View(await phongTroMVCContext.ToListAsync());
        }

        // GET: VatDung/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VatDungs == null)
            {
                return NotFound();
            }

            var vatDung = await _context.VatDungs
                .Include(v => v.IdPhongNavigation)
                .FirstOrDefaultAsync(m => m.IdVatdung == id);
            if (vatDung == null)
            {
                return NotFound();
            }

            return View(vatDung);
        }

        // GET: VatDung/Create
        public IActionResult Create()
        {
            ViewData["IdPhong"] = new SelectList(_context.Phongs, "IdPhong", "IdPhong");
            return View();
        }

        // POST: VatDung/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVatdung,TenVatdung,IdPhong")] VatDung vatDung)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vatDung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPhong"] = new SelectList(_context.Phongs, "IdPhong", "IdPhong", vatDung.IdPhong);
            return View(vatDung);
        }

        // GET: VatDung/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VatDungs == null)
            {
                return NotFound();
            }

            var vatDung = await _context.VatDungs.FindAsync(id);
            if (vatDung == null)
            {
                return NotFound();
            }
            ViewData["IdPhong"] = new SelectList(_context.Phongs, "IdPhong", "IdPhong", vatDung.IdPhong);
            return View(vatDung);
        }

        // POST: VatDung/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVatdung,TenVatdung,IdPhong")] VatDung vatDung)
        {
            if (id != vatDung.IdVatdung)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vatDung);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VatDungExists(vatDung.IdVatdung))
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
            ViewData["IdPhong"] = new SelectList(_context.Phongs, "IdPhong", "IdPhong", vatDung.IdPhong);
            return View(vatDung);
        }

        // GET: VatDung/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VatDungs == null)
            {
                return NotFound();
            }

            var vatDung = await _context.VatDungs
                .Include(v => v.IdPhongNavigation)
                .FirstOrDefaultAsync(m => m.IdVatdung == id);
            if (vatDung == null)
            {
                return NotFound();
            }

            return View(vatDung);
        }

        // POST: VatDung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VatDungs == null)
            {
                return Problem("Entity set 'PhongTroMVCContext.VatDungs'  is null.");
            }
            var vatDung = await _context.VatDungs.FindAsync(id);
            if (vatDung != null)
            {
                _context.VatDungs.Remove(vatDung);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VatDungExists(int id)
        {
          return (_context.VatDungs?.Any(e => e.IdVatdung == id)).GetValueOrDefault();
        }
    }
}
