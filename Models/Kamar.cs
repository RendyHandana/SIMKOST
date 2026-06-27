using System.ComponentModel.DataAnnotations;

namespace SIMKOST.Models
{
    public class Kamar
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nomor kamar wajib diisi")]
        [StringLength(10)]
        public string NomorKamar { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lantai wajib diisi")]
        public int Lantai { get; set; }

        [Required(ErrorMessage = "Harga wajib diisi")]
        public decimal Harga { get; set; }

        [Required]
        public string Status { get; set; } = "Kosong";

        // Relasi ke tabel Sewa
        public ICollection<Sewa>? Sewa { get; set; }
    }
}