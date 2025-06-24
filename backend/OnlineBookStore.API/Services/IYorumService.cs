using OnlineBookStore.API.Models;

namespace OnlineBookStore.API.Services
{
    /// <summary>
    /// Yorum işlemlerinin iş mantığı arayüzü.
    /// </summary>
    public interface IYorumService
    {
        Task<IEnumerable<Yorum>> GetByKitapIdAsync(int kitapId);
        Task<Yorum> GetByIdAsync(int id);
        Task AddAsync(Yorum yorum);
        Task UpdateAsync(Yorum yorum);
        Task DeleteAsync(int id);
    }
} 