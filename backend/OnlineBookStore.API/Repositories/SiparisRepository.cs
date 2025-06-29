using Microsoft.EntityFrameworkCore;
using OnlineBookStore.API.Models;
using OnlineBookStore.API.Data;

namespace OnlineBookStore.API.Repositories
{
    /// <summary>
    /// Entity Framework ile sipariş işlemlerini gerçekleştiren repository.
    /// </summary>
    public class SiparisRepository : ISiparisRepository
    {
        private readonly AppDbContext _context;
        public SiparisRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Siparis>> GetByKullaniciIdAsync(int kullaniciId)
        {
            return await _context.Siparisler.Include(s => s.Kalemler).Where(s => s.KullaniciId == kullaniciId).ToListAsync();
        }

        public async Task<IEnumerable<Siparis>> GetAllAsync()
        {
            return await _context.Siparisler.Include(s => s.Kalemler).ToListAsync();
        }

        public async Task<Siparis> GetByIdAsync(int id)
        {
            return await _context.Siparisler.Include(s => s.Kalemler).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Siparis siparis)
        {
            await _context.Siparisler.AddAsync(siparis);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var siparis = await _context.Siparisler.Include(s => s.Kalemler).FirstOrDefaultAsync(s => s.Id == id);
            if (siparis != null)
            {
                _context.Siparisler.Remove(siparis);
                await _context.SaveChangesAsync();
            }
        }
    }
} 