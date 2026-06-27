using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMKOST.Migrations
{
    /// <inheritdoc />
    public partial class AddPembayaran : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pembayaran",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SewaId = table.Column<int>(type: "int", nullable: false),
                    TanggalBayar = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    JumlahBayar = table.Column<decimal>(type: "decimal(18,0)", precision: 18, scale: 0, nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pembayaran", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pembayaran_Sewa_SewaId",
                        column: x => x.SewaId,
                        principalTable: "Sewa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Pembayaran_SewaId",
                table: "Pembayaran",
                column: "SewaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pembayaran");
        }
    }
}
