using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMKOST.Models
{
    public class Pembayaran
    {
        public int Id { get; set; }

        [Required]
        public int SewaId { get; set; }

        // Kita gunakan 'TanggalBayar' agar cocok dengan kode di Controller & View Anda
        [Required]
        [Column("Tanggal")] // Tetap memetakan ke kolom 'Tanggal' yang ada di database
        public DateTime TanggalBayar { get; set; } 

        // Menambahkan kolom baru
        public DateTime? TanggalJatuhTempo { get; set; } 

        [Required]
        [Column(TypeName = "decimal(18,0)")]
        public decimal JumlahBayar { get; set; }

        [Required]
        public string Status { get; set; } = "Lunas";

        public Sewa? Sewa { get; set; }
    }
}