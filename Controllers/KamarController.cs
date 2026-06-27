using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIMKOST.Data;
using SIMKOST.Models;

namespace SIMKOST.Controllers
{
    public class KamarController : Controller
    {
        private readonly AppDbContext _context;

        public KamarController(AppDbContext context)
        {
            _context = context;
        }

        private bool IsLoggedIn() => HttpContext.Session.GetString("Username") != null;

        public async Task<IActionResult> Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            return View(await _context.Kamar.ToListAsync());
        }

        public IActionResult Create() => IsLoggedIn() ? View() : RedirectToAction("Login", "Account");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kamar kamar)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {
                _context.Add(kamar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kamar);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (id == null) return NotFound();

            var kamar = await _context.Kamar.FindAsync(id);
            if (kamar == null) return NotFound();
            return View(kamar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Kamar kamar)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (id != kamar.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(kamar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kamar);
        }

        // Action ini dipanggil langsung oleh tombol Hapus di Index.cshtml
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            var kamar = await _context.Kamar.FindAsync(id);
            if (kamar != null)
            {
                _context.Kamar.Remove(kamar);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}