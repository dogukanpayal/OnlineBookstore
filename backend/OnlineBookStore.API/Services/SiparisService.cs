using OnlineBookStore.API.Models;
using OnlineBookStore.API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookStore.API.Services
{
    /// <summary>
    /// Sipariş işlemlerinin iş mantığı katmanı.
    /// </summary>
    public class SiparisService : ISiparisService
    {
        private readonly ISiparisRepository _siparisRepository;
        public SiparisService(ISiparisRepository siparisRepository)
        {
            _siparisRepository = siparisRepository;
        }

        public async Task<IEnumerable<Siparis>> GetByKullaniciIdAsync(int kullaniciId)
        {
            return await _siparisRepository.GetByKullaniciIdAsync(kullaniciId);
        }

        public async Task<IEnumerable<Siparis>> GetAllAsync()
        {
            return await _siparisRepository.GetAllAsync();
        }

        public async Task<Siparis> GetByIdAsync(int id)
        {
            return await _siparisRepository.GetByIdAsync(id);
        }

        public async Task<Siparis> CreateAsync(int kullaniciId, List<(int kitapId, int adet, decimal fiyat)> kalemler)
        {
            var siparis = new Siparis
            {
                KullaniciId = kullaniciId,
                Tarih = DateTime.Now,
                Kalemler = kalemler.Select(k => new SiparisKalem
                {
                    KitapId = k.kitapId,
                    Adet = k.adet,
                    Fiyat = k.fiyat
                }).ToList(),
                ToplamTutar = kalemler.Sum(k => k.fiyat * k.adet)
            };
            await _siparisRepository.AddAsync(siparis);
            return siparis;
        }

        public async Task DeleteAsync(int id)
        {
            await _siparisRepository.DeleteAsync(id);
        }
    }
} 