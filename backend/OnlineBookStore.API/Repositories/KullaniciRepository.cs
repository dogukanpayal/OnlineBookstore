using Microsoft.EntityFrameworkCore;
using OnlineBookStore.API.Models;
using OnlineBookStore.API.Data;

namespace OnlineBookStore.API.Repositories
{
    /// <summary>
    /// Entity Framework ile kullanıcı işlemlerini gerçekleştiren repository.
    /// </summary>
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly AppDbContext _context;

        public KullaniciRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Kullanici> GetByEmailAsync(string email)
        {
            return await _context.Kullanicilar.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Kullanici> GetByIdAsync(int id)
        {
            return await _context.Kullanicilar.FindAsync(id);
        }

        public async Task AddAsync(Kullanici kullanici)
        {
            await _context.Kullanicilar.AddAsync(kullanici);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyByEmailAsync(string email)
        {
            return await _context.Kullanicilar.AnyAsync(x => x.Email == email);
        }

        public async Task UpdateAsync(Kullanici kullanici)
        {
            _context.Kullanicilar.Update(kullanici);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Kullanici>> GetAllAsync()
        {
            return await _context.Kullanicilar.ToListAsync();
        }
    }
} 