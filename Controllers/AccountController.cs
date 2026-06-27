using Microsoft.AspNetCore.Mvc;
using SIMKOST.Data;
using SIMKOST.Models;

namespace SIMKOST.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.User.FirstOrDefault(x =>
                x.Username == model.Username &&
                x.Password == model.Password);

            if (user == null)
            {
                ViewBag.Error = "Username atau Password salah";
                return View(model);
            }

            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}