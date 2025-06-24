using OnlineBookStore.API.Models;
using OnlineBookStore.API.Repositories;

namespace OnlineBookStore.API.Services
{
    /// <summary>
    /// Kitap işlemlerinin iş mantığı katmanı.
    /// </summary>
    public class KitapService : IKitapService
    {
        private readonly IKitapRepository _kitapRepository;

        public KitapService(IKitapRepository kitapRepository)
        {
            _kitapRepository = kitapRepository;
        }

        public async Task<IEnumerable<Kitap>> GetAllAsync()
        {
            return await _kitapRepository.GetAllAsync();
        }

        public async Task<Kitap> GetByIdAsync(int id)
        {
            return await _kitapRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Kitap kitap)
        {
            await _kitapRepository.AddAsync(kitap);
        }

        public async Task UpdateAsync(Kitap kitap)
        {
            await _kitapRepository.UpdateAsync(kitap);
        }

        public async Task DeleteAsync(int id)
        {
            await _kitapRepository.DeleteAsync(id);
        }

        public async Task<(IEnumerable<Kitap> kitaplar, int toplamKayit)> SearchAsync(string? arama, string? kategori, int sayfa, int sayfaBoyutu, string? sirala)
        {
            return await _kitapRepository.SearchAsync(arama, kategori, sayfa, sayfaBoyutu, sirala);
        }
    }
} 