using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspProject.Migrations
{
    /// <inheritdoc />
    public partial class NewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Urunler_UrunId",
                table: "Kullanicilar");

            migrationBuilder.RenameColumn(
                name: "UrunId",
                table: "Kullanicilar",
                newName: "SehirId");

            migrationBuilder.RenameIndex(
                name: "IX_Kullanicilar_UrunId",
                table: "Kullanicilar",
                newName: "IX_Kullanicilar_SehirId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Sehirler_SehirId",
                table: "Kullanicilar",
                column: "SehirId",
                principalTable: "Sehirler",
                principalColumn: "SehirNo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Sehirler_SehirId",
                table: "Kullanicilar");

            migrationBuilder.RenameColumn(
                name: "SehirId",
                table: "Kullanicilar",
                newName: "UrunId");

            migrationBuilder.RenameIndex(
                name: "IX_Kullanicilar_SehirId",
                table: "Kullanicilar",
                newName: "IX_Kullanicilar_UrunId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Urunler_UrunId",
                table: "Kullanicilar",
                column: "UrunId",
                principalTable: "Urunler",
                principalColumn: "UrunId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
