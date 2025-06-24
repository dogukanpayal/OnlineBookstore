using OnlineBookStore.API.Models;
using OnlineBookStore.API.DTOs;

namespace OnlineBookStore.API.Mappers
{
    /// <summary>
    /// Kullanici ile KullaniciDto arasında dönüşüm işlemlerini yapar.
    /// </summary>
    public static class KullaniciMapper
    {
        public static KullaniciDto ToDto(Kullanici kullanici)
        {
            return new KullaniciDto
            {
                Id = kullanici.Id,
                AdSoyad = kullanici.AdSoyad,
                Email = kullanici.Email,
                Rol = kullanici.Rol
            };
        }
    }
} 