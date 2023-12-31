﻿        using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPhongTro.Models;

namespace WebPhongTro.Controllers
{

    public class HomeController : Controller
    {
        private readonly PhongTroMVCContext _phongTroMVCContext;
        public HomeController(PhongTroMVCContext phongTroMVCContext)
        {
            _phongTroMVCContext = phongTroMVCContext;
        }
        public IActionResult Index()
        {
           
            var phongs = _phongTroMVCContext.Phongs;
            return View(phongs);
        }
        public IActionResult About()
        {
            return View();
        }
        // GET: Phongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _phongTroMVCContext.Phongs == null)
            {
                return NotFound();
            }

            var phong = await _phongTroMVCContext.Phongs.Include(p => p.VatDungs)
                .FirstOrDefaultAsync(m => m.IdPhong == id);
            if (phong == null)
            {
                return NotFound();
            }

            return View(phong);
        }
        [HttpPost]
        public IActionResult Index(string timkiem)
        {
            if(timkiem != null)
            {
                var phongs = _phongTroMVCContext.Phongs.Where(p => p.TenPhong == timkiem);
                return View(phongs);
            }  else
            {
                var phongs = _phongTroMVCContext.Phongs;
                return View(phongs);
            }    
          
        }
    }
}
