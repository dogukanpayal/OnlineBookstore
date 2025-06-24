using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace OnlineBookStore.API.Models
{
    /// <summary>
    /// Sipariş ana modeli.
    /// </summary>
    public class Siparis
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int KullaniciId { get; set; }

        public DateTime Tarih { get; set; } = DateTime.Now;

        public decimal ToplamTutar { get; set; }

        public List<SiparisKalem> Kalemler { get; set; }
    }

    /// <summary>
    /// Sipariş içindeki kitaplar.
    /// </summary>
    public class SiparisKalem
    {
        [Key]
        public int Id { get; set; }
        public int SiparisId { get; set; }
        public int KitapId { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
    }
} 