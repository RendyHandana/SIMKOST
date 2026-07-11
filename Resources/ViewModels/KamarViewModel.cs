namespace SIMKOST.Resources.ViewModels
{
    public class KamarViewModel
    {
        public int Id { get; set; }

        public string NomorKamar { get; set; } = string.Empty;

        // Diubah menjadi int
        public int Lantai { get; set; } 

        public decimal Harga { get; set; }

        public string Status { get; set; } = string.Empty;

        public string? HargaFormatRupiah { get; set; }
    }
}