using Microsoft.AspNetCore.Mvc;
using SIMKOST.Data;
using SIMKOST.Models;
using System.Linq;
using System.IO; 
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore; // Penting untuk .Include()

namespace SIMKOST.Controllers
{
    public class PenghuniController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public PenghuniController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // --- INDEX: Menampilkan data dengan relasi Sewa agar IsAktif berfungsi ---
        public IActionResult Index()
        {
            // .Include(p => p.Sewa) memastikan data sewa dimuat ke dalam model Penghuni
            var daftarPenghuni = _context.Penghuni.Include(p => p.Sewa).ToList();
            return View(daftarPenghuni);
        }

        // --- CREATE: Tampilkan Form (GET) ---
        [HttpGet]
        public IActionResult Create() => View();

        // --- CREATE: Simpan Data (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Penghuni penghuni, IFormFile? fileKtp)
        {
            if (ModelState.IsValid)
            {
                if (fileKtp != null && fileKtp.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                    
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileKtp.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        fileKtp.CopyTo(fileStream);
                    }
                    penghuni.FotoKTP = uniqueFileName;
                }

                _context.Add(penghuni);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(penghuni);
        }

        // --- EDIT: Tampilkan Form (GET) ---
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var penghuni = _context.Penghuni.Find(id);
            if (penghuni == null) return NotFound();
            return View(penghuni);
        }

        // --- EDIT: Simpan Perubahan (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Penghuni penghuniTerupdate, IFormFile? fileKtp)
        {
            var penghuniLama = _context.Penghuni.Find(id);
            if (penghuniLama == null) return NotFound();

            if (ModelState.IsValid)
            {
                penghuniLama.Nama = penghuniTerupdate.Nama;
                penghuniLama.NIK = penghuniTerupdate.NIK;
                penghuniLama.NoHP = penghuniTerupdate.NoHP;
                penghuniLama.Alamat = penghuniTerupdate.Alamat;

                if (fileKtp != null && fileKtp.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileKtp.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        fileKtp.CopyTo(fileStream);
                    }
                    penghuniLama.FotoKTP = uniqueFileName;
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(penghuniTerupdate);
        }
    }
}