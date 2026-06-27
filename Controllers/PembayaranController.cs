using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIMKOST.Data;
using SIMKOST.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SIMKOST.Controllers
{
    public class PembayaranController : Controller
    {
        private readonly AppDbContext _context;

        public PembayaranController(AppDbContext context)
        {
            _context = context;
        }

        // 1. LIST DATA PEMBAYARAN
        public async Task<IActionResult> Index()
        {
            var data = await _context.Pembayaran
                .Include(p => p.Sewa != null ? p.Sewa.Penghuni : null)
                .Include(p => p.Sewa != null ? p.Sewa.Kamar : null)
                .ToListAsync();

            return View(data);
        }

        // 2. CETAK INVOICE (FITUR YANG HILANG DITAMBAHKAN KEMBALI)
        public async Task<IActionResult> CetakInvoice(int id)
        {
            var pembayaran = await _context.Pembayaran
                .Include(p => p.Sewa != null ? p.Sewa.Penghuni : null)
                .Include(p => p.Sewa != null ? p.Sewa.Kamar : null)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pembayaran == null) return NotFound();

            return View(pembayaran);
        }

        // 3. TAMBAH PEMBAYARAN (GET)
        public IActionResult Create()
        {
            ViewBag.SewaId = _context.Sewa.Include(s => s.Penghuni)
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Penghuni != null ? s.Penghuni.Nama : "-" })
                .ToList();
            return View();
        }

        // 4. TAMBAH PEMBAYARAN (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pembayaran pembayaran)
        {
            if (ModelState.IsValid)
            {
                pembayaran.TanggalJatuhTempo = pembayaran.TanggalBayar.AddMonths(1);
                _context.Pembayaran.Add(pembayaran);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pembayaran);
        }

        // 5. EDIT PEMBAYARAN (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var pembayaran = await _context.Pembayaran.FindAsync(id);
            if (pembayaran == null) return NotFound();

            ViewBag.SewaId = new SelectList(_context.Sewa.Include(s => s.Penghuni), "Id", "Penghuni.Nama", pembayaran.SewaId);
            return View(pembayaran);
        }

        // 6. EDIT PEMBAYARAN (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pembayaran pembayaran)
        {
            if (id != pembayaran.Id) return NotFound();

            if (ModelState.IsValid)
            {
                pembayaran.TanggalJatuhTempo = pembayaran.TanggalBayar.AddMonths(1);
                _context.Update(pembayaran);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pembayaran);
        }

        // 7. HAPUS PEMBAYARAN
        public async Task<IActionResult> Delete(int id)
        {
            var pembayaran = await _context.Pembayaran.FindAsync(id);
            if (pembayaran != null)
            {
                _context.Pembayaran.Remove(pembayaran);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}