using System.ComponentModel.DataAnnotations;

namespace SIMKOST.Models
{
    public class Sewa
    {
        public int Id { get; set; }

        [Required]
        public int PenghuniId { get; set; }

        [Required]
        public int KamarId { get; set; }

        [Required]
        public DateTime TanggalMasuk { get; set; }

        public DateTime? TanggalKeluar { get; set; }

        // Relasi ke Penghuni
        public Penghuni? Penghuni { get; set; }

        // Relasi ke Kamar
        public Kamar? Kamar { get; set; }

        // Relasi ke Pembayaran
        public ICollection<Pembayaran>? Pembayaran { get; set; }
    }
}