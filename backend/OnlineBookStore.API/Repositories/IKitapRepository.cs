using OnlineBookStore.API.Models;

namespace OnlineBookStore.API.Repositories
{
    /// <summary>
    /// Kitap veritabanı işlemleri için arayüz.
    /// </summary>
    public interface IKitapRepository
    {
        Task<IEnumerable<Kitap>> GetAllAsync();
        Task<Kitap> GetByIdAsync(int id);
        Task AddAsync(Kitap kitap);
        Task UpdateAsync(Kitap kitap);
        Task DeleteAsync(int id);
        Task<(IEnumerable<Kitap> kitaplar, int toplamKayit)> SearchAsync(string? arama, string? kategori, int sayfa, int sayfaBoyutu, string? sirala);
        // Arama, filtreleme, sayfalama vs. eklenebilir
    }
} 