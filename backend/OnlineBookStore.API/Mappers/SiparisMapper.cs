using OnlineBookStore.API.Models;
using OnlineBookStore.API.DTOs;
using System.Linq;
using System.Collections.Generic;

namespace OnlineBookStore.API.Mappers
{
    /// <summary>
    /// Siparis ve SiparisKalem ile ilgili DTO dönüşümlerini yapar.
    /// </summary>
    public static class SiparisMapper
    {
        public static SiparisDto ToDto(Siparis siparis, List<Kitap> kitaplar)
        {
            return new SiparisDto
            {
                Id = siparis.Id,
                SiparisTarihi = siparis.Tarih,
                ToplamTutar = siparis.ToplamTutar,
                SiparisDetaylari = siparis.Kalemler?.Select(kalem =>
                    ToDto(kalem, kitaplar.FirstOrDefault(k => k.Id == kalem.KitapId))
                ).ToList() ?? new List<SiparisKalemDto>(),
                KullaniciId = siparis.KullaniciId,
                KullaniciAdSoyad = "-" // Kullanıcı bilgisi yok
            };
        }

        public static SiparisDto ToDto(Siparis siparis, List<Kitap> kitaplar, List<Kullanici> kullanicilar)
        {
            var kullanici = kullanicilar.FirstOrDefault(k => k.Id == siparis.KullaniciId);
            return new SiparisDto
            {
                Id = siparis.Id,
                SiparisTarihi = siparis.Tarih,
                ToplamTutar = siparis.ToplamTutar,
                SiparisDetaylari = siparis.Kalemler?.Select(kalem =>
                    ToDto(kalem, kitaplar.FirstOrDefault(k => k.Id == kalem.KitapId))
                ).ToList() ?? new List<SiparisKalemDto>(),
                KullaniciId = siparis.KullaniciId,
                KullaniciAdSoyad = kullanici?.AdSoyad ?? "-"
            };
        }

        public static SiparisKalemDto ToDto(SiparisKalem kalem, Kitap kitap)
        {
            return new SiparisKalemDto
            {
                Id = kalem.Id,
                KitapId = kalem.KitapId,
                Adet = kalem.Adet,
                Fiyat = kalem.Fiyat,
                Kitap = kitap != null ? KitapMapper.ToDto(kitap) : null
            };
        }
    }
} 