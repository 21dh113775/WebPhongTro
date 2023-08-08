using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebPhongTro.Models;
using WebPhongTro.Models.Domain;

namespace WebPhongTro.Controllers
{
    public class TrangCaNhanController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly PhongTroMVCContext _phongTroMVCContext;

        public TrangCaNhanController(UserManager<ApplicationUser> userManager, PhongTroMVCContext phongTroMVCContext)
        {
            _userManager = userManager;
            _phongTroMVCContext = phongTroMVCContext;
        }

        public IActionResult Index()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            string id = _userManager.GetUserId(User);
            var user = _phongTroMVCContext.AspNetUsers.Where(u => u.Id == id).FirstOrDefault();
            return View(user);
        }
        public IActionResult HopDong()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            string id = _userManager.GetUserId(User);
            var user = _phongTroMVCContext.AspNetUsers.Include(u => u.IdHds).ThenInclude(hd => hd.IdUsers).FirstOrDefault(u => u.Id == id);

            return View(user);
        }
    }
}
