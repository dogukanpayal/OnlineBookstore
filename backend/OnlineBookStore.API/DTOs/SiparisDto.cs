using System;
using System.Collections.Generic;

namespace OnlineBookStore.API.DTOs
{
    /// <summary>
    /// Sipariş verisinin istemciye aktarımı için DTO.
    /// </summary>
    public class SiparisDto
    {
        public int Id { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public decimal ToplamTutar { get; set; }
        public List<SiparisKalemDto> SiparisDetaylari { get; set; }
        public int KullaniciId { get; set; }
        public string KullaniciAdSoyad { get; set; }
    }

    /// <summary>
    /// Sipariş içindeki kitaplar için DTO.
    /// </summary>
    public class SiparisKalemDto
    {
        public int Id { get; set; }
        public int KitapId { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public KitapDto Kitap { get; set; }
    }
} 