using OnlineBookStore.API.Models;

namespace OnlineBookStore.API.Repositories
{
    /// <summary>
    /// Sipariş işlemleri için repository arayüzü.
    /// </summary>
    public interface ISiparisRepository
    {
        Task<IEnumerable<Siparis>> GetByKullaniciIdAsync(int kullaniciId);
        Task<IEnumerable<Siparis>> GetAllAsync(); // Admin için
        Task<Siparis> GetByIdAsync(int id);
        Task AddAsync(Siparis siparis);
    }
} 