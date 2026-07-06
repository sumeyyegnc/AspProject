using AspProject.Models;
using Microsoft.AspNetCore.Mvc;

public class RaporController : Controller
{
    private readonly SistemDbContext _context;

    public RaporController(SistemDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(string type = "kullanicilar")
    {
        ViewBag.Type = type;

        var model = new RaporViewModel
        {
            Kullanicilar = _context.Kullanicilar.ToList(),
            Sehirler = _context.Sehirler.ToList(),
            Urunler = _context.Urunler.ToList()
        };

        return View(model);
    }
}