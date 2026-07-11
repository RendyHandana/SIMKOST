using System.ComponentModel.DataAnnotations;

namespace SIMKOST.Models
{
    public class Kamar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NomorKamar { get; set; } = string.Empty;

        // Diubah menjadi int agar sesuai dengan int(11) di MySQL
        [Required]
        public int Lantai { get; set; } 

        [Required]
        public decimal Harga { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        public ICollection<Sewa>? Sewa { get; set; }
    }
}