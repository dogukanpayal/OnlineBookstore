using OnlineBookStore.API.Models;

namespace OnlineBookStore.API.Repositories
{
    /// <summary>
    /// Kullanıcı veritabanı işlemleri için arayüz.
    /// </summary>
    public interface IKullaniciRepository
    {
        Task<Kullanici> GetByEmailAsync(string email);
        Task<Kullanici> GetByIdAsync(int id);
        Task AddAsync(Kullanici kullanici);
        Task<bool> AnyByEmailAsync(string email);
        Task UpdateAsync(Models.Kullanici kullanici);
    }
} 