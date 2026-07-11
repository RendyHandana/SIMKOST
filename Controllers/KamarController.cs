using Microsoft.AspNetCore.Mvc;
using SIMKOST.Services;
using SIMKOST.Resources.ViewModels; // <-- Diubah agar sesuai dengan namespace baru ViewModel Anda

namespace SIMKOST.Controllers
{
    public class KamarController : Controller
    {
        private readonly IKamarService _kamarService;

        // Inject IKamarService ke dalam Constructor
        public KamarController(IKamarService kamarService)
        {
            _kamarService = kamarService;
        }

        // Helper untuk mengecek status login session
        private bool IsLoggedIn() => HttpContext.Session.GetString("Username") != null;

        // GET: Kamar
        public async Task<IActionResult> Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            
            // Mengambil data melalui Service
            var daftarKamar = await _kamarService.GetDaftarKamarAsync();
            return View(daftarKamar);
        }

        // GET: Kamar/Create
        public IActionResult Create() => IsLoggedIn() ? View() : RedirectToAction("Login", "Account");

        // POST: Kamar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KamarViewModel kamarVm)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            
            if (ModelState.IsValid)
            {
                // Proses simpan diserahkan ke Service
                await _kamarService.AddKamarAsync(kamarVm);
                return RedirectToAction(nameof(Index));
            }
            return View(kamarVm);
        }

        // GET: Kamar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (id == null) return NotFound();

            // Mengambil data spesifik berdasarkan ID melalui Service
            var kamarVm = await _kamarService.GetKamarByIdAsync(id.Value);
            if (kamarVm == null) return NotFound();
            
            return View(kamarVm);
        }

        // POST: Kamar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KamarViewModel kamarVm)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (id != kamarVm.Id) return NotFound();

            if (ModelState.IsValid)
            {
                // Proses update diserahkan ke Service
                await _kamarService.UpdateKamarAsync(kamarVm);
                return RedirectToAction(nameof(Index));
            }
            return View(kamarVm);
        }

        // GET/POST: Kamar/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            // Proses penghapusan diserahkan ke Service
            await _kamarService.DeleteKamarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}