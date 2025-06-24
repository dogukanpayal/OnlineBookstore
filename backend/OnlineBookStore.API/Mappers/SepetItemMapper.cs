using OnlineBookStore.API.Models;
using OnlineBookStore.API.DTOs;

namespace OnlineBookStore.API.Mappers
{
    /// <summary>
    /// SepetItem ile SepetItemDto arasında dönüşüm işlemlerini yapar.
    /// </summary>
    public static class SepetItemMapper
    {
        public static SepetItemDto ToDto(SepetItem item)
        {
            return new SepetItemDto
            {
                Id = item.Id,
                KitapId = item.KitapId,
                Adet = item.Adet
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