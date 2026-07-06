using AspProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspProject.Controllers
{
    public class SehirlerController : Controller
    {
        public readonly SistemDbContext dbContext;

        public SehirlerController(SistemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public IActionResult Index(string searchString)
        {
            var result = dbContext.Sehirler.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => x.SehirAdi.Contains(searchString));
            }

            return View(result.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Sehir sehirler)
        {
            dbContext.Sehirler.Add(sehirler);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var result = dbContext.Sehirler.Find(id);
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(Sehir sehirler)
        {
            var mevcut = dbContext.Sehirler.Find(sehirler.SehirNo);

            mevcut.SehirAdi = sehirler.SehirAdi;
            mevcut.Plaka = sehirler.Plaka;

            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var result = dbContext.Sehirler.Find(id);
            return View(result);
        }

        [HttpPost]
        public IActionResult Delete(Sehir sehirler)
        {
            dbContext.Remove(sehirler);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}