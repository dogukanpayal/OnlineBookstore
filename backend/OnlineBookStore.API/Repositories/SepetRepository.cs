using OnlineBookStore.API.Models;
using OnlineBookStore.API.Data;
using Microsoft.EntityFrameworkCore;

namespace OnlineBookStore.API.Repositories
{
    /// <summary>
    /// Entity Framework ile sepet işlemlerini gerçekleştiren repository.
    /// </summary>
    public class SepetRepository : ISepetRepository
    {
        private readonly AppDbContext _context;
        public SepetRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SepetItem>> GetByKullaniciIdAsync(int kullaniciId)
        {
            return await _context.Sepetler.Where(s => s.KullaniciId == kullaniciId).ToListAsync();
        }

        public async Task<SepetItem> GetByIdAsync(int id)
        {
            return await _context.Sepetler.FindAsync(id);
        }

        public async Task<SepetItem> GetByKullaniciAndKitapAsync(int kullaniciId, int kitapId)
        {
            return await _context.Sepetler.FirstOrDefaultAsync(s => s.KullaniciId == kullaniciId && s.KitapId == kitapId);
        }

        public async Task AddAsync(SepetItem item)
        {
            await _context.Sepetler.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SepetItem item)
        {
            _context.Sepetler.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Sepetler.FindAsync(id);
            if (item != null)
            {
                _context.Sepetler.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAllAsync(int kullaniciId)
        {
            var items = _context.Sepetler.Where(s => s.KullaniciId == kullaniciId);
            _context.Sepetler.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
} 