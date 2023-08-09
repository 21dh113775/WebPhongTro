using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPhongTro.Models;

namespace WebPhongTro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HoaDonController : Controller
    {
        private readonly PhongTroMVCContext _context;

        public HoaDonController(PhongTroMVCContext context)
        {
            _context = context;
        }

        // GET: HoaDon
        public async Task<IActionResult> Index()
        {
            var phongTroMVCContext = _context.HoaDons.Include(h => h.IdHopdongNavigation);
            return View(await phongTroMVCContext.ToListAsync());
        }

        // GET: HoaDon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HoaDons == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.IdHopdongNavigation)
                .FirstOrDefaultAsync(m => m.IdHoadon == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // GET: HoaDon/Create
        public IActionResult Create()
        {
            ViewData["IdHopdong"] = new SelectList(_context.HopDongs, "IdHopdong", "IdHopdong");
            return View();
        }

        // POST: HoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHoadon,IdHopdong,NgayLap,TienDien,TienNuoc")] HoaDon hoaDon)
        {   
            if (ModelState.IsValid)
            {
                var hopDong = _context.HopDongs.Include(h => h.IdPhongNavigation).FirstOrDefault(h => h.IdHopdong == hoaDon.IdHopdong);
                if(hopDong.IdPhongNavigation != null)
                {
                    int giaPhong = (int)hopDong.IdPhongNavigation.GiaPhong;
                    hoaDon.TongTien = giaPhong + hoaDon.TienDien + hoaDon.TienNuoc;
                }

                _context.Add(hoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHopdong"] = new SelectList(_context.HopDongs, "IdHopdong", "IdHopdong", hoaDon.IdHopdong);
            return View(hoaDon);
        }

        // GET: HoaDon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HoaDons == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            ViewData["IdHopdong"] = new SelectList(_context.HopDongs, "IdHopdong", "IdHopdong", hoaDon.IdHopdong);
            return View(hoaDon);
        }

        // POST: HoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHoadon,IdHopdong,NgayLap,TienDien,TienNuoc,TongTien")] HoaDon hoaDon)
        {
            if (id != hoaDon.IdHoadon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonExists(hoaDon.IdHoadon))
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
            ViewData["IdHopdong"] = new SelectList(_context.HopDongs, "IdHopdong", "IdHopdong", hoaDon.IdHopdong);
            return View(hoaDon);
        }

        // GET: HoaDon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HoaDons == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.IdHopdongNavigation)
                .FirstOrDefaultAsync(m => m.IdHoadon == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // POST: HoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HoaDons == null)
            {
                return Problem("Entity set 'PhongTroMVCContext.HoaDons'  is null.");
            }
            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon != null)
            {
                _context.HoaDons.Remove(hoaDon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonExists(int id)
        {
          return (_context.HoaDons?.Any(e => e.IdHoadon == id)).GetValueOrDefault();
        }
    }
}
