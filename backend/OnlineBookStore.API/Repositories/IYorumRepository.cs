using OnlineBookStore.API.Models;

namespace OnlineBookStore.API.Repositories
{
    /// <summary>
    /// Yorum veritabanı işlemleri için arayüz.
    /// </summary>
    public interface IYorumRepository
    {
        Task<IEnumerable<Yorum>> GetByKitapIdAsync(int kitapId);
        Task<Yorum> GetByIdAsync(int id);
        Task AddAsync(Yorum yorum);
        Task UpdateAsync(Yorum yorum);
        Task DeleteAsync(int id);
    }
} 