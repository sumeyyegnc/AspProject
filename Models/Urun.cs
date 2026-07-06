using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspProject.Models
{
    public class Urun
    {

        [Key]
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int Fiyati { get; set; }
        public int Stok { get; set; }

        [ForeignKey("sehir")]
        public int SehirNo { get; set; }
        public virtual Sehir sehir { get; set; }
    }
}
