using OnlineBookStore.API.Models;
using OnlineBookStore.API.DTOs;

namespace OnlineBookStore.API.Mappers
{
    /// <summary>
    /// Kitap ile KitapDto arasında dönüşüm işlemlerini yapar.
    /// </summary>
    public static class KitapMapper
    {
        public static KitapDto ToDto(Kitap kitap)
        {
            return new KitapDto
            {
                Id = kitap.Id,
                Ad = kitap.Ad,
                Yazar = kitap.Yazar,
                Fiyat = kitap.Fiyat,
                Kategori = kitap.Kategori,
                KapakResmiUrl = kitap.KapakResmiUrl,
                Aciklama = kitap.Aciklama
            };
        }

        public static Kitap ToModel(KitapDto dto)
        {
            return new Kitap
            {
                Id = dto.Id,
                Ad = dto.Ad,
                Yazar = dto.Yazar,
                Fiyat = dto.Fiyat,
                Kategori = dto.Kategori,
                KapakResmiUrl = dto.KapakResmiUrl,
                Aciklama = dto.Aciklama
            };
        }
    }
} 