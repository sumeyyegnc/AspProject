using System.ComponentModel.DataAnnotations;

namespace AspProject.Models
{
    public class Sehir
    {
        [Key]
        public int SehirNo { get; set; }
        public string SehirAdi { get; set; }
        public int Plaka { get; set; }




    }
}
