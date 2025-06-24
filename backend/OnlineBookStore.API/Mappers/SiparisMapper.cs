using OnlineBookStore.API.Models;
using OnlineBookStore.API.DTOs;
using System.Linq;

namespace OnlineBookStore.API.Mappers
{
    /// <summary>
    /// Siparis ve SiparisKalem ile ilgili DTO dönüşümlerini yapar.
    /// </summary>
    public static class SiparisMapper
    {
        public static SiparisDto ToDto(Siparis siparis)
        {
            return new SiparisDto
            {
                Id = siparis.Id,
                Tarih = siparis.Tarih,
                ToplamTutar = siparis.ToplamTutar,
                Kalemler = siparis.Kalemler?.Select(ToDto).ToList() ?? new List<SiparisKalemDto>()
            };
        }
        public static SiparisKalemDto ToDto(SiparisKalem kalem)
        {
            return new SiparisKalemDto
            {
                KitapId = kalem.KitapId,
                Adet = kalem.Adet,
                Fiyat = kalem.Fiyat
            };
        }
    }
} 