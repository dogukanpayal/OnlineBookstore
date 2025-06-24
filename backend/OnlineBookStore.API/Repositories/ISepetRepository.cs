using OnlineBookStore.API.Models;

namespace OnlineBookStore.API.Repositories
{
    /// <summary>
    /// Sepet işlemleri için repository arayüzü.
    /// </summary>
    public interface ISepetRepository
    {
        Task<IEnumerable<SepetItem>> GetByKullaniciIdAsync(int kullaniciId);
        Task<SepetItem> GetByIdAsync(int id);
        Task<SepetItem> GetByKullaniciAndKitapAsync(int kullaniciId, int kitapId);
        Task AddAsync(SepetItem item);
        Task UpdateAsync(SepetItem item);
        Task DeleteAsync(int id);
        Task DeleteAllAsync(int kullaniciId);
    }
} 