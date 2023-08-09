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
            ViewBag.Role = _context.AspNetRoles;
            ViewBag.User = _context.AspNetUsers;
            return View();
        }
        [HttpPost]
        public IActionResult Index(string idUser, string idQuyen)
        {
            var user = _context.AspNetUsers.FirstOrDefault(x => x.Id == idUser);
            var role = _context.AspNetRoles.FirstOrDefault(x => x.Id == idQuyen);

            if (user != null && role != null)
            {
                user.Roles.Add(role);
                _context.Update(user);
                role.Users.Add(user);
                _context.Update(role);
            }

            _context.SaveChanges();

            ViewBag.Role = _context.AspNetRoles;
            ViewBag.User = _context.AspNetUsers;
            return View();
        }
    }
}
