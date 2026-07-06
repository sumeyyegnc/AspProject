using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspProject.Migrations
{
    /// <inheritdoc />
    public partial class News : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UrunId",
                table: "Kullanicilar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_UrunId",
                table: "Kullanicilar",
                column: "UrunId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Urunler_UrunId",
                table: "Kullanicilar",
                column: "UrunId",
                principalTable: "Urunler",
                principalColumn: "UrunId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Urunler_UrunId",
                table: "Kullanicilar");

            migrationBuilder.DropIndex(
                name: "IX_Kullanicilar_UrunId",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "UrunId",
                table: "Kullanicilar");
        }
    }
}
