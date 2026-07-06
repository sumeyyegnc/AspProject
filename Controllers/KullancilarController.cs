using AspProject.Migrations;
using AspProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Reflection.Metadata;
using QuestPDF.Fluent;
using OfficeOpenXml.Style;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Document = QuestPDF.Fluent.Document;
using OfficeOpenXml;
namespace AspProject.Controllers
{
    public class KullanicilarController : Controller
    {
        public readonly SistemDbContext dbContext;

        public KullanicilarController(SistemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // 🔎 LİSTE + ARAMA
        public IActionResult Index(string searchString)
        {
            var result = dbContext.Kullanicilar.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => x.KullaniciAdi.Contains(searchString));
            }

            return View(result.ToList());
        }

        // ➕ CREATE (GET)
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        // ➕ CREATE (POST)
        [HttpPost]
        public IActionResult Create(Kullanici kullanici)
        {
            dbContext.Kullanicilar.Add(kullanici);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // ✏️ EDIT (GET)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = dbContext.Kullanicilar.Find(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // ✏️ EDIT (POST)
        [HttpPost]
        public IActionResult Edit(Kullanici kullanici)
        {
            var mevcut = dbContext.Kullanicilar.Find(kullanici.KullaniciId);

            if (mevcut == null)
                return NotFound();

            mevcut.KullaniciAdi = kullanici.KullaniciAdi;
            mevcut.Mail = kullanici.Mail;

            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // ❌ DELETE (GET)
        public IActionResult Delete(int id)
        {
            var user = dbContext.Kullanicilar.Find(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // ❌ DELETE (POST)
        [HttpPost]
        public IActionResult Delete(Kullanici kullanici)
        {
            var mevcut = dbContext.Kullanicilar.Find(kullanici.KullaniciId);

            if (mevcut != null)
            {
                dbContext.Kullanicilar.Remove(mevcut);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult ExportToPdf()
        {
            var users = dbContext.Kullanicilar.ToList();

            var pdfDocument = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));

                    // HEADER
                    page.Header()
                        .Text("Kullanıcı Listesi Raporu")
                        .SemiBold()
                        .FontSize(20)
                        .FontColor(Colors.Blue.Medium);

                    // CONTENT
                    page.Content()
                        .PaddingTop(1, Unit.Centimetre)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(50);   // ID
                                columns.RelativeColumn();     // Ad
                                columns.RelativeColumn();     // Mail
                                columns.RelativeColumn();     // Şifre
                            });

                            // Başlıklar
                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("ID").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Kullanıcı Adı").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Mail").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Şifre").Bold();
                            });

                            // Veriler
                            foreach (var item in users)
                            {
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5)
                                    .Text(item.KullaniciId.ToString());

                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5)
                                    .Text(item.KullaniciAdi);

                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5)
                                    .Text(item.Mail);

                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5)
                                    .Text(item.Sifre);
                            }
                        });

                    // FOOTER
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Sayfa ");
                            x.CurrentPageNumber();
                        });
                });
            });

            var pdfBytes = pdfDocument.GeneratePdf();

            return File(pdfBytes, "application/pdf",
                $"Kullanici_Listesi_{DateTime.Now:yyyyMMdd}.pdf");
        }

            public IActionResult ExportToExcel()
        {
            ExcelPackage.License.SetNonCommercialPersonal("Backend softito");

            var users = dbContext.Kullanicilar.ToList();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Kullanıcı Listesi");

                // 📌 Başlıklar
                worksheet.Cells[1, 1].Value = "Kullanıcı ID";
                worksheet.Cells[1, 2].Value = "Kullanıcı Adı";
                worksheet.Cells[1, 3].Value = "Mail";
                worksheet.Cells[1, 4].Value = "Şifre";

                // 🎨 Başlık stil
                using (var range = worksheet.Cells[1, 1, 1, 4])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(41, 128, 185));
                    range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // 📊 Veriler
                int row = 2;

                foreach (var item in users)
                {
                    worksheet.Cells[row, 1].Value = item.KullaniciId;
                    worksheet.Cells[row, 2].Value = item.KullaniciAdi;
                    worksheet.Cells[row, 3].Value = item.Mail;
                    worksheet.Cells[row, 4].Value = item.Sifre; // istersen kaldır

                    row++;
                }

                // 📏 Otomatik genişlik
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // 📁 dosya oluştur
                var fileBytes = package.GetAsByteArray();
                string fileName = $"Kullanici_Listesi_{DateTime.Now:yyyyMMdd}.xlsx";

                return File(fileBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName);
            }
        }
    }
    
}