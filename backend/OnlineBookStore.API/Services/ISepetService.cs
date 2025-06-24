using OnlineBookStore.API.Models;

namespace OnlineBookStore.API.Services
{
    /// <summary>
    /// Sepet işlemlerinin iş mantığı arayüzü.
    /// </summary>
    public interface ISepetService
    {
        Task<IEnumerable<SepetItem>> GetByKullaniciIdAsync(int kullaniciId);
        Task AddOrUpdateAsync(int kullaniciId, int kitapId, int adet);
        Task DeleteAsync(int id);
        Task DeleteAllAsync(int kullaniciId);
    }
} 