using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspProject.Migrations
{
    /// <inheritdoc />
    public partial class LastChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Sehirler_SehirId",
                table: "Kullanicilar");

            migrationBuilder.DropIndex(
                name: "IX_Kullanicilar_SehirId",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "SehirId",
                table: "Kullanicilar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SehirId",
                table: "Kullanicilar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_SehirId",
                table: "Kullanicilar",
                column: "SehirId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Sehirler_SehirId",
                table: "Kullanicilar",
                column: "SehirId",
                principalTable: "Sehirler",
                principalColumn: "SehirNo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
