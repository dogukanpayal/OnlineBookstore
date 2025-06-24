using Microsoft.EntityFrameworkCore;
using OnlineBookStore.API.Models;
using OnlineBookStore.API.Data;

namespace OnlineBookStore.API.Repositories
{
    /// <summary>
    /// Entity Framework ile yorum işlemlerini gerçekleştiren repository.
    /// </summary>
    public class YorumRepository : IYorumRepository
    {
        private readonly AppDbContext _context;

        public YorumRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Yorum>> GetByKitapIdAsync(int kitapId)
        {
            return await _context.Yorumlar.Where(y => y.KitapId == kitapId).ToListAsync();
        }

        public async Task<Yorum> GetByIdAsync(int id)
        {
            return await _context.Yorumlar.FindAsync(id);
        }

        public async Task AddAsync(Yorum yorum)
        {
            await _context.Yorumlar.AddAsync(yorum);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Yorum yorum)
        {
            _context.Yorumlar.Update(yorum);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var yorum = await _context.Yorumlar.FindAsync(id);
            if (yorum != null)
            {
                _context.Yorumlar.Remove(yorum);
                await _context.SaveChangesAsync();
            }
        }
    }
} 