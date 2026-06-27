using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMKOST.Migrations
{
    /// <inheritdoc />
    public partial class AddSewa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sewa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PenghuniId = table.Column<int>(type: "int", nullable: false),
                    KamarId = table.Column<int>(type: "int", nullable: false),
                    TanggalMasuk = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TanggalKeluar = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sewa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sewa_Kamar_KamarId",
                        column: x => x.KamarId,
                        principalTable: "Kamar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sewa_Penghuni_PenghuniId",
                        column: x => x.PenghuniId,
                        principalTable: "Penghuni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Sewa_KamarId",
                table: "Sewa",
                column: "KamarId");

            migrationBuilder.CreateIndex(
                name: "IX_Sewa_PenghuniId",
                table: "Sewa",
                column: "PenghuniId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sewa");
        }
    }
}
