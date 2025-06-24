using OnlineBookStore.API.Models;

namespace OnlineBookStore.API.Services
{
    /// <summary>
    /// Kitap işlemlerinin iş mantığı arayüzü.
    /// </summary>
    public interface IKitapService
    {
        Task<IEnumerable<Kitap>> GetAllAsync();
        Task<Kitap> GetByIdAsync(int id);
        Task AddAsync(Kitap kitap);
        Task UpdateAsync(Kitap kitap);
        Task DeleteAsync(int id);
        Task<(IEnumerable<Kitap> kitaplar, int toplamKayit)> SearchAsync(string? arama, string? kategori, int sayfa, int sayfaBoyutu, string? sirala);
    }
} 