using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIMKOST.Data;
using SIMKOST.Models;

namespace SIMKOST.Controllers
{
    public class SewaController : Controller
    {
        private readonly AppDbContext _context;

        public SewaController(AppDbContext context)
        {
            _context = context;
        }

        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("Username") != null;
        }

        public async Task<IActionResult> Index()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var data = await _context.Sewa
                .Include(s => s.Penghuni)
                .Include(s => s.Kamar)
                .ToListAsync();

            return View(data);
        }

        public IActionResult Create()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            ViewBag.PenghuniId = new SelectList(
                _context.Penghuni,
                "Id",
                "Nama"
            );

            ViewBag.KamarId = new SelectList(
                _context.Kamar.Where(k => k.Status == "Kosong"),
                "Id",
                "NomorKamar"
            );

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sewa sewa)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Sewa.Add(sewa);

                var kamar = await _context.Kamar.FindAsync(sewa.KamarId);

                if (kamar != null)
                {
                    kamar.Status = "Terisi";
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.PenghuniId = new SelectList(
                _context.Penghuni,
                "Id",
                "Nama",
                sewa.PenghuniId
            );

            ViewBag.KamarId = new SelectList(
                _context.Kamar.Where(k => k.Status == "Kosong"),
                "Id",
                "NomorKamar",
                sewa.KamarId
            );

            return View(sewa);
        }
    }
}