using OnlineBookStore.API.Models;
using OnlineBookStore.API.Repositories;

namespace OnlineBookStore.API.Services
{
    /// <summary>
    /// Yorum işlemlerinin iş mantığı katmanı.
    /// </summary>
    public class YorumService : IYorumService
    {
        private readonly IYorumRepository _yorumRepository;

        public YorumService(IYorumRepository yorumRepository)
        {
            _yorumRepository = yorumRepository;
        }

        public async Task<IEnumerable<Yorum>> GetByKitapIdAsync(int kitapId)
        {
            return await _yorumRepository.GetByKitapIdAsync(kitapId);
        }

        public async Task<Yorum> GetByIdAsync(int id)
        {
            return await _yorumRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Yorum yorum)
        {
            await _yorumRepository.AddAsync(yorum);
        }

        public async Task UpdateAsync(Yorum yorum)
        {
            await _yorumRepository.UpdateAsync(yorum);
        }

        public async Task DeleteAsync(int id)
        {
            await _yorumRepository.DeleteAsync(id);
        }
    }
} 