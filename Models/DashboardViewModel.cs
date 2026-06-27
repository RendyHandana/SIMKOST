using System.Collections.Generic;

namespace SIMKOST.Models
{
    public class DashboardViewModel
    {
        // Statistik Utama
        public int TotalKamar { get; set; }

        public int TotalPenghuni { get; set; }

        public int TotalSewa { get; set; }

        public int TotalPembayaran { get; set; }

        public int KamarKosong { get; set; }

        public int KamarTerisi { get; set; }

        public decimal TotalPendapatan { get; set; }

        // Dashboard Modern
        public decimal PendapatanBulanIni { get; set; }

        public int TagihanBelumBayar { get; set; }

        // Data Terbaru
        public List<Penghuni>? PenghuniTerbaru { get; set; }

        public List<Pembayaran>? PembayaranTerbaru { get; set; }

        // Grafik Pendapatan Bulanan
        public List<string>? BulanGrafik { get; set; }

        public List<decimal>? PendapatanGrafik { get; set; }
    }
}