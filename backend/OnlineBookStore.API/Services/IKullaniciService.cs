using OnlineBookStore.API.Models;

namespace OnlineBookStore.API.Services
{
    /// <summary>
    /// Kullanıcı işlemlerinin iş mantığı arayüzü.
    /// </summary>
    public interface IKullaniciService
    {
        Task<Kullanici> RegisterAsync(string adSoyad, string email, string sifre);
        Task<Kullanici> LoginAsync(string email, string sifre);
        Task<Kullanici> GetByIdAsync(int id);
        Task UpdateAsync(Models.Kullanici kullanici);
        Task<List<Kullanici>> GetAllAsync();
    }
} 