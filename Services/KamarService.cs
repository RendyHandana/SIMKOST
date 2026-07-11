using Microsoft.EntityFrameworkCore;
using SIMKOST.Data;
using SIMKOST.Models;
using SIMKOST.Resources.ViewModels;

namespace SIMKOST.Services
{
    public class KamarService : IKamarService
    {
        private readonly AppDbContext _context;

        public KamarService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<KamarViewModel>> GetDaftarKamarAsync()
        {
            return await _context.Kamar
                .Select(k => new KamarViewModel
                {
                    Id = k.Id,
                    NomorKamar = k.NomorKamar ?? string.Empty,
                    Lantai = k.Lantai, // Bersih tanpa ?? string.Empty
                    Harga = k.Harga,
                    Status = k.Status ?? string.Empty,
                    HargaFormatRupiah = $"Rp {k.Harga:N0}"
                }).ToListAsync();
        }

        public async Task<KamarViewModel> GetKamarByIdAsync(int id)
        {
            var k = await _context.Kamar.FindAsync(id);
            if (k == null) return new KamarViewModel();

            return new KamarViewModel
            {
                Id = k.Id,
                NomorKamar = k.NomorKamar ?? string.Empty,
                Lantai = k.Lantai,
                Harga = k.Harga,
                Status = k.Status ?? string.Empty
            };
        }

        public async Task AddKamarAsync(KamarViewModel kamarVm)
        {
            var kamar = new Kamar 
            { 
                NomorKamar = kamarVm.NomorKamar, 
                Lantai = kamarVm.Lantai,
                Harga = kamarVm.Harga,
                Status = kamarVm.Status
            };
            _context.Add(kamar);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateKamarAsync(KamarViewModel kamarVm)
        {
            var kamar = await _context.Kamar.FindAsync(kamarVm.Id);
            if (kamar != null)
            {
                kamar.NomorKamar = kamarVm.NomorKamar;
                kamar.Lantai = kamarVm.Lantai;
                kamar.Harga = kamarVm.Harga;
                kamar.Status = kamarVm.Status;
                
                _context.Update(kamar);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteKamarAsync(int id)
        {
            var kamar = await _context.Kamar.FindAsync(id);
            if (kamar != null)
            {
                _context.Kamar.Remove(kamar);
                await _context.SaveChangesAsync();
            }
        }
    }
}