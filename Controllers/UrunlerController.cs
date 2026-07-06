using AspProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspProject.Controllers
{
    public class UrunlerController : Controller
    {
        public readonly SistemDbContext dbContext;

        public UrunlerController(SistemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

       
        public IActionResult Index(string searchString)
        {
            var result = dbContext.Urunler.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => x.UrunAdi.Contains(searchString));
            }

            return View(result.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Urun urunler)
        {
            dbContext.Urunler.Add(urunler);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var result = dbContext.Urunler.Find(id);
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(Urun urunler)
        {
            var mevcut = dbContext.Urunler.Find(urunler.UrunId);

            mevcut.UrunAdi = urunler.UrunAdi;
            mevcut.Stok = urunler.Stok;

            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var result = dbContext.Urunler.Find(id);
            return View(result);
        }

        [HttpPost]
        public IActionResult Delete(Urun urunler)
        {
            dbContext.Remove(urunler);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}