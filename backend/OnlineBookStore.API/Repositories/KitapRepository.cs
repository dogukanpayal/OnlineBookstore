using Microsoft.EntityFrameworkCore;
using OnlineBookStore.API.Models;
using OnlineBookStore.API.Data;

namespace OnlineBookStore.API.Repositories
{
    /// <summary>
    /// Entity Framework ile kitap işlemlerini gerçekleştiren repository.
    /// </summary>
    public class KitapRepository : IKitapRepository
    {
        private readonly AppDbContext _context;

        public KitapRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Kitap>> GetAllAsync()
        {
            return await _context.Kitaplar.ToListAsync();
        }

        public async Task<Kitap> GetByIdAsync(int id)
        {
            return await _context.Kitaplar.FindAsync(id);
        }

        public async Task AddAsync(Kitap kitap)
        {
            await _context.Kitaplar.AddAsync(kitap);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Kitap kitap)
        {
            _context.Kitaplar.Update(kitap);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var kitap = await _context.Kitaplar.FindAsync(id);
            if (kitap != null)
            {
                _context.Kitaplar.Remove(kitap);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<(IEnumerable<Kitap> kitaplar, int toplamKayit)> SearchAsync(string? arama, string? kategori, int sayfa, int sayfaBoyutu, string? sirala)
        {
            var query = _context.Kitaplar.AsQueryable();

            if (!string.IsNullOrWhiteSpace(arama))
                query = query.Where(k => k.Ad.Contains(arama) || k.Yazar.Contains(arama));

            if (!string.IsNullOrWhiteSpace(kategori))
                query = query.Where(k => k.Kategori == kategori);

            // Sıralama
            if (!string.IsNullOrWhiteSpace(sirala))
            {
                switch (sirala.ToLower())
                {
                    case "fiyat_asc":
                        query = query.OrderBy(k => k.Fiyat);
                        break;
                    case "fiyat_desc":
                        query = query.OrderByDescending(k => k.Fiyat);
                        break;
                    case "ad_asc":
                        query = query.OrderBy(k => k.Ad);
                        break;
                    case "ad_desc":
                        query = query.OrderByDescending(k => k.Ad);
                        break;
                    default:
                        query = query.OrderBy(k => k.Id);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(k => k.Id);
            }

            var toplamKayit = await query.CountAsync();
            var kitaplar = await query.Skip((sayfa - 1) * sayfaBoyutu).Take(sayfaBoyutu).ToListAsync();
            return (kitaplar, toplamKayit);
        }
    }
} 