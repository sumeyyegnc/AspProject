using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspProject.Migrations
{
    /// <inheritdoc />
    public partial class NewMg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ÜrünAdi",
                table: "Urunler",
                newName: "UrunAdi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrunAdi",
                table: "Urunler",
                newName: "ÜrünAdi");
        }
    }
}
