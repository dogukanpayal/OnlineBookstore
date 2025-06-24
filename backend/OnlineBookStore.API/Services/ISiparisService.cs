using OnlineBookStore.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineBookStore.API.Services
{
    /// <summary>
    /// Sipariş işlemlerinin iş mantığı arayüzü.
    /// </summary>
    public interface ISiparisService
    {
        Task<IEnumerable<Siparis>> GetByKullaniciIdAsync(int kullaniciId);
        Task<IEnumerable<Siparis>> GetAllAsync(); // Admin için
        Task<Siparis> GetByIdAsync(int id);
        Task<Siparis> CreateAsync(int kullaniciId, List<(int kitapId, int adet, decimal fiyat)> kalemler);
    }
} 