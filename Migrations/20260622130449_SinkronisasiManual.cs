using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMKOST.Migrations
{
    /// <inheritdoc />
    public partial class SinkronisasiManual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Dikosongkan agar tidak terjadi konflik dengan tabel yang sudah ada di database
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Dikosongkan karena tidak ada perubahan yang perlu dibatalkan
        }
    }
}