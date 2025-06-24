using Microsoft.EntityFrameworkCore;
using OnlineBookStore.API.Models;

namespace OnlineBookStore.API.Data
{
    /// <summary>
    /// Entity Framework Core için veritabanı bağlamı.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }
        public DbSet<SepetItem> Sepetler { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<SiparisKalem> SiparisKalemleri { get; set; }

        // Diğer DbSet'ler (Kullanıcı, Sipariş, Yorum, Sepet vs.) buraya eklenebilir
    }
} 