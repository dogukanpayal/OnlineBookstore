using OnlineBookStore.API.Models;
using OnlineBookStore.API.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace OnlineBookStore.API.Services
{
    /// <summary>
    /// Kullanıcı işlemlerinin iş mantığı katmanı.
    /// </summary>
    public class KullaniciService : IKullaniciService
    {
        private readonly IKullaniciRepository _kullaniciRepository;

        public KullaniciService(IKullaniciRepository kullaniciRepository)
        {
            _kullaniciRepository = kullaniciRepository;
        }

        public async Task<Kullanici> RegisterAsync(string adSoyad, string email, string sifre)
        {
            if (await _kullaniciRepository.AnyByEmailAsync(email))
                throw new Exception("Bu e-posta ile kayıtlı bir kullanıcı zaten var.");

            var kullanici = new Kullanici
            {
                AdSoyad = adSoyad,
                Email = email,
                SifreHash = HashSifre(sifre),
                Rol = "Uye"
            };
            await _kullaniciRepository.AddAsync(kullanici);
            return kullanici;
        }

        public async Task<Kullanici> LoginAsync(string email, string sifre)
        {
            var kullanici = await _kullaniciRepository.GetByEmailAsync(email);
            if (kullanici == null || kullanici.SifreHash != HashSifre(sifre))
                throw new Exception("E-posta veya şifre hatalı.");
            return kullanici;
        }

        public async Task<Kullanici> GetByIdAsync(int id)
        {
            return await _kullaniciRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Kullanici kullanici)
        {
            // Sadece ad-soyad güncellenebilir
            // E-posta ve rol değiştirilemez
            // Şifre güncelleme için ayrı bir metot gerekir
            await _kullaniciRepository.UpdateAsync(kullanici);
        }

        public async Task<List<Kullanici>> GetAllAsync()
        {
            return (await _kullaniciRepository.GetAllAsync()).ToList();
        }

        // Şifre hashleme için basit bir örnek (gerçek projede daha güvenli yöntemler kullanılmalı)
        private string HashSifre(string sifre)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(sifre);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
} 