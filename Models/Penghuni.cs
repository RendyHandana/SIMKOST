using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Tambahkan ini

namespace SIMKOST.Models
{
    public class Penghuni
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama wajib diisi")]
        [Display(Name = "Nama Lengkap")]
        public string Nama { get; set; } = string.Empty;

        [Required(ErrorMessage = "NIK wajib diisi")]
        public string NIK { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nomor HP wajib diisi")]
        [Display(Name = "Nomor Handphone")]
        public string NoHP { get; set; } = string.Empty;

        [Required(ErrorMessage = "Alamat wajib diisi")]
        public string Alamat { get; set; } = string.Empty;

        [Display(Name = "Foto KTP")]
        public string? FotoKTP { get; set; }

        public ICollection<Sewa>? Sewa { get; set; }

        // Properti ini tidak masuk ke database, hanya digunakan untuk tampilan
        [NotMapped]
        public bool IsAktif => Sewa != null && Sewa.Any();
    }
}