using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SIMKOST.Data;
using SIMKOST.Models;
using System.Linq;
using System.Collections.Generic;

namespace SIMKOST.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Proteksi Login
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var totalPendapatan = _context.Pembayaran.Any()
                ? _context.Pembayaran.Sum(p => p.JumlahBayar)
                : 0;

            var model = new DashboardViewModel
            {
                TotalKamar = _context.Kamar.Count(),
                TotalPenghuni = _context.Penghuni.Count(),
                TotalSewa = _context.Sewa.Count(),
                TotalPembayaran = _context.Pembayaran.Count(),
                KamarKosong = _context.Kamar.Count(k => k.Status == "Kosong"),
                KamarTerisi = _context.Kamar.Count(k => k.Status == "Terisi"),
                TotalPendapatan = totalPendapatan
            };

            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            return View(model);
        }

        // ==========================================================================
        // FITUR PENCARIAN GLOBAL AJAX (TOPBAR)
        // ==========================================================================
        [HttpGet]
        public IActionResult GlobalSearch(string query)
        {
            // Proteksi agar fitur tidak bisa diakses jika belum login
            if (HttpContext.Session.GetString("Username") == null)
            {
                return Json(new { success = false, message = "Unauthorized" });
            }

            if (string.IsNullOrWhiteSpace(query))
            {
                return Json(new { success = true, data = new List<object>() });
            }

            var lowerQuery = query.ToLower();
            var searchResults = new List<object>();

            // 1. Cari data berdasarkan Nama Penghuni
            var hasilPenghuni = _context.Penghuni
                .Where(p => p.Nama.ToLower().Contains(lowerQuery))
                .Select(p => new {
                    kategori = "Penghuni",
                    teks = p.Nama,
                    url = "/Penghuni" // Mengarah ke daftar penghuni (bisa diganti /Penghuni/Details/id jika ada)
                })
                .Take(5)
                .ToList();
            searchResults.AddRange(hasilPenghuni);

            // 2. Cari data berdasarkan Nomor Kamar
            var hasilKamar = _context.Kamar
                .Where(k => k.NomorKamar.ToLower().Contains(lowerQuery))
                .Select(k => new {
                    kategori = "Kamar",
                    teks = "Kamar No. " + k.NomorKamar + " (" + k.Status + ")",
                    url = "/Kamar"
                })
                .Take(5)
                .ToList();
            searchResults.AddRange(hasilKamar);

            // 3. Cari data berdasarkan Kode atau Detail Sewa (Opsional)
            var hasilSewa = _context.Sewa
                .Where(s => s.Id.ToString().Contains(lowerQuery))
                .Select(s => new {
                    kategori = "Kontrak Sewa",
                    teks = "ID Sewa: " + s.Id,
                    url = "/Sewa"
                })
                .Take(3)
                .ToList();
            searchResults.AddRange(hasilSewa);

            return Json(new { success = true, data = searchResults });
        }
    }
}