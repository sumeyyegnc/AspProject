using AspProject.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AspProject.Controllers
{
    public class ReportController : Controller
    {
        public readonly SistemDbContext dbContext;
        public ReportController(SistemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var result = (from u in dbContext.Urunler
                          join s in dbContext.Sehirler
            on u.SehirNo equals s.SehirNo
                          select new urunsehirlistview
                          {
                              UrunAdi = u.UrunAdi,
                              Fiyati = u.Fiyati,
                              Stok = u.Stok,
                              SehirAdi = s.SehirAdi,
                          }).ToList();
            return View(result);
        }


        public IActionResult SehirRapor()
        {

            var result = (from u in dbContext.Urunler
                          join s in dbContext.Sehirler
                          on u.SehirNo equals s.SehirNo
                          group u by s.SehirAdi into g
                          select new urunsehirlistview
                          {
                              SehirAdi = g.Key,
                              Stok = g.Count()
                          }).ToList();

            return View(result);
        }


     







    }
}
