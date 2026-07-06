using System.Collections.Generic;

namespace AspProject.Models
{
    public class RaporViewModel
    {
        public List<Kullanici> Kullanicilar { get; set; }
        public List<Sehir> Sehirler { get; set; }
        public List<Urun> Urunler { get; set; }
    }
}