using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPhongTro.Models;

namespace WebPhongTro.Controllers
{
    public class PhanQuyenController : Controller
    {
        private readonly PhongTroMVCContext _context;

        public PhanQuyenController(PhongTroMVCContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var user = _context.AspNetUsers;
            ViewBag.Role = _context.AspNetRoles;
            ViewBag.User = _context.AspNetUsers;
            return View(user);
        }


    }
}
