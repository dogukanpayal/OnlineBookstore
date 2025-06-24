using OnlineBookStore.API.Models;
using OnlineBookStore.API.DTOs;

namespace OnlineBookStore.API.Mappers
{
    /// <summary>
    /// Yorum ile YorumDto arasında dönüşüm işlemlerini yapar.
    /// </summary>
    public static class YorumMapper
    {
        public static YorumDto ToDto(Yorum yorum)
        {
            return new YorumDto
            {
                Id = yorum.Id,
                KitapId = yorum.KitapId,
                KullaniciId = yorum.KullaniciId,
                Icerik = yorum.Icerik,
                Puan = yorum.Puan,
                Tarih = yorum.Tarih
            };
        }
        public static Yorum ToModel(YorumDto dto)
        {
            return new Yorum
            {
                Id = dto.Id,
                KitapId = dto.KitapId,
                KullaniciId = dto.KullaniciId,
                Icerik = dto.Icerik,
                Puan = dto.Puan,
                Tarih = dto.Tarih
            };
        }
    }
} 