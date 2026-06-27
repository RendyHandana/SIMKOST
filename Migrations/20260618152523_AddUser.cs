using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMKOST.Migrations
{
    /// <inheritdoc />
    public partial class AddUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sewa_Kamar_KamarId",
                table: "Sewa");

            migrationBuilder.DropForeignKey(
                name: "FK_Sewa_Penghuni_PenghuniId",
                table: "Sewa");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Sewa_Kamar_KamarId",
                table: "Sewa",
                column: "KamarId",
                principalTable: "Kamar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sewa_Penghuni_PenghuniId",
                table: "Sewa",
                column: "PenghuniId",
                principalTable: "Penghuni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sewa_Kamar_KamarId",
                table: "Sewa");

            migrationBuilder.DropForeignKey(
                name: "FK_Sewa_Penghuni_PenghuniId",
                table: "Sewa");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_Sewa_Kamar_KamarId",
                table: "Sewa",
                column: "KamarId",
                principalTable: "Kamar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sewa_Penghuni_PenghuniId",
                table: "Sewa",
                column: "PenghuniId",
                principalTable: "Penghuni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
