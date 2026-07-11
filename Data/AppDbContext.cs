using Microsoft.EntityFrameworkCore;
using SIMKOST.Models;

namespace SIMKOST.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tabel User
        public DbSet<User> User { get; set; }

        // Tabel Kamar
        public DbSet<Kamar> Kamar { get; set; }

        // Tabel Penghuni
        public DbSet<Penghuni> Penghuni { get; set; }

        // Tabel Sewa
        public DbSet<Sewa> Sewa { get; set; }

        // Tabel Pembayaran
        public DbSet<Pembayaran> Pembayaran { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfigurasi Tabel Kamar
            modelBuilder.Entity<Kamar>(entity =>
            {
                // Presisi Harga Kamar
                entity.Property(k => k.Harga)
                      .HasPrecision(18, 0);
            });

            // Relasi Sewa -> Penghuni
            modelBuilder.Entity<Sewa>()
                .HasOne(s => s.Penghuni)
                .WithMany(p => p.Sewa)
                .HasForeignKey(s => s.PenghuniId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relasi Sewa -> Kamar
            modelBuilder.Entity<Sewa>()
                .HasOne(s => s.Kamar)
                .WithMany(k => k.Sewa)
                .HasForeignKey(s => s.KamarId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relasi Pembayaran -> Sewa
            modelBuilder.Entity<Pembayaran>()
                .HasOne(p => p.Sewa)
                .WithMany(s => s.Pembayaran)
                .HasForeignKey(p => p.SewaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Presisi Jumlah Bayar
            modelBuilder.Entity<Pembayaran>()
                .Property(p => p.JumlahBayar)
                .HasPrecision(18, 0);
        }
    }
}