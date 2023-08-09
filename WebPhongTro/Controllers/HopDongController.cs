using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebPhongTro.Models;

namespace WebPhongTro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HopDongController : Controller
    {
        private readonly PhongTroMVCContext _context;

        public HopDongController(PhongTroMVCContext context)
        {
            _context = context;
        }

        // GET: HopDong
        public async Task<IActionResult> Index()
        {
            var phongTroMVCContext = _context.HopDongs.Include(h => h.IdPhongNavigation);
            return View(await phongTroMVCContext.ToListAsync());
        }

        // GET: HopDong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HopDongs == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDongs
                .Include(h => h.IdPhongNavigation)
                .FirstOrDefaultAsync(m => m.IdHopdong == id);
            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        // GET: HopDong/Create
        public IActionResult Create()
        {
            ViewBag.Phong = _context.Phongs;
            ViewBag.User = _context.AspNetUsers;
            ViewBag.CountUser = _context.AspNetUsers.Count();
            return View();
           
        }

        // POST: HopDong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( List<string> idUsers ,[Bind("IdHopdong,IdPhong,NgayBatDau,NgayKetThuc,IdUsers")] HopDong hopDong)
        {
            var test = ModelState.IsValid;


            if (ModelState.IsValid)
            {
                foreach(var item in idUsers)
                {
                    var user = _context.AspNetUsers.Where(u => u.Id == item).FirstOrDefault();
                    hopDong.IdUsers.Add(user);
                    user.IdHds.Add(hopDong);
                }    
                _context.Add(hopDong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Phong = _context.Phongs;
            ViewBag.User = _context.AspNetUsers;
            ViewBag.CountUser = _context.AspNetUsers.Count();
            return View(hopDong);
        }

        // GET: HopDong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HopDongs == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDongs.FindAsync(id);
            if (hopDong == null)
            {
                return NotFound();
            }
            ViewData["IdPhong"] = new SelectList(_context.Phongs, "IdPhong", "IdPhong", hopDong.IdPhong);
            ViewBag.User = _context.AspNetUsers;
            return View(hopDong);
        }

        // POST: HopDong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHopdong,IdPhong,IdKhach,NgayBatDau,NgayKetThuc")] HopDong hopDong)
        {
            if (id != hopDong.IdHopdong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hopDong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HopDongExists(hopDong.IdHopdong))
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
            ViewData["IdPhong"] = new SelectList(_context.Phongs, "IdPhong", "IdPhong", hopDong.IdPhong);
            return View(hopDong);
        }

        // GET: HopDong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HopDongs == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDongs
                .Include(h => h.IdPhongNavigation)
                .FirstOrDefaultAsync(m => m.IdHopdong == id);
            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        // POST: HopDong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HopDongs == null)
            {
                return Problem("Entity set 'PhongTroMVCContext.HopDongs'  is null.");
            }
            var hopDong = await _context.HopDongs.FindAsync(id);
            if (hopDong != null)
            {
                _context.HopDongs.Remove(hopDong);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HopDongExists(int id)
        {
          return (_context.HopDongs?.Any(e => e.IdHopdong == id)).GetValueOrDefault();
        }
    }
}
