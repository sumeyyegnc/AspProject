using Microsoft.EntityFrameworkCore;

namespace AspProject.Models
{
    public class SistemDbContext:DbContext
    {

        public SistemDbContext(DbContextOptions<SistemDbContext> dbContext) : base(dbContext)
        { 
        
        }

        public DbSet<Kullanici>Kullanicilar { get; set; }
        public DbSet<Urun>Urunler { get; set; }
        public DbSet<Sehir>Sehirler { get; set; }


    }
}
