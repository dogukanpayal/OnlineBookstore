using OnlineBookStore.API.Models;
using OnlineBookStore.API.Repositories;

namespace OnlineBookStore.API.Services
{
    /// <summary>
    /// Sepet işlemlerinin iş mantığı katmanı.
    /// </summary>
    public class SepetService : ISepetService
    {
        private readonly ISepetRepository _sepetRepository;
        public SepetService(ISepetRepository sepetRepository)
        {
            _sepetRepository = sepetRepository;
        }

        public async Task<IEnumerable<SepetItem>> GetByKullaniciIdAsync(int kullaniciId)
        {
            return await _sepetRepository.GetByKullaniciIdAsync(kullaniciId);
        }

        public async Task AddOrUpdateAsync(int kullaniciId, int kitapId, int adet)
        {
            var mevcut = await _sepetRepository.GetByKullaniciAndKitapAsync(kullaniciId, kitapId);
            if (mevcut != null)
            {
                mevcut.Adet = adet;
                await _sepetRepository.UpdateAsync(mevcut);
            }
            else
            {
                var yeni = new SepetItem { KullaniciId = kullaniciId, KitapId = kitapId, Adet = adet };
                await _sepetRepository.AddAsync(yeni);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _sepetRepository.DeleteAsync(id);
        }

        public async Task DeleteAllAsync(int kullaniciId)
        {
            await _sepetRepository.DeleteAllAsync(kullaniciId);
        }
    }
} 