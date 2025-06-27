using OnlineBookStore.API.Models;
using OnlineBookStore.API.DTOs;

namespace OnlineBookStore.API.Mappers
{
    /// <summary>
    /// SepetItem ile SepetItemDto arasında dönüşüm işlemlerini yapar.
    /// </summary>
    public static class SepetItemMapper
    {
        public static SepetItemDto ToDto(SepetItem item, Kitap kitap = null)
        {
            return new SepetItemDto
            {
                Id = item.Id,
                KitapId = item.KitapId,
                Adet = item.Adet,
                Kitap = kitap != null ? new KitapDto
                {
                    Id = kitap.Id,
                    Ad = kitap.Ad,
                    Yazar = kitap.Yazar,
                    Fiyat = kitap.Fiyat,
                    Kategori = kitap.Kategori,
                    KapakResmiUrl = kitap.KapakResmiUrl,
                    Aciklama = kitap.Aciklama
                } : null
            };
        }
        public static SepetItem ToModel(SepetItemDto dto, int kullaniciId)
        {
            return new SepetItem
            {
                Id = dto.Id,
                KitapId = dto.KitapId,
                Adet = dto.Adet,
                KullaniciId = kullaniciId
            };
        }
    }
} 