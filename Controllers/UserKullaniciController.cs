using AspProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspProject.Controllers
{
    public class UserKullaniciController : Controller
    {
        public readonly SistemDbContext dbContext;

        public UserKullaniciController(SistemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

     
        public IActionResult Index(string searchString)
        {
            var result = dbContext.Kullanicilar.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => x.KullaniciAdi.Contains(searchString));
            }

            return View(result.ToList());
        }

      
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Kullanici kullanici)
        {
            dbContext.Kullanicilar.Add(kullanici);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        
        
        }
    }
