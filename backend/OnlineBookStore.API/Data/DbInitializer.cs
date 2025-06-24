using OnlineBookStore.API.Models;

namespace OnlineBookStore.API.Data
{
    /// <summary>
    /// Veritabanı ilk verilerini ekler.
    /// </summary>
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Kitaplar.Any())
                return; // Zaten veri var

            var kitaplar = new Kitap[]
            {
                new Kitap { Ad = "Suç ve Ceza", Yazar = "Fyodor Dostoyevski", Fiyat = 120, Kategori = "Roman", KapakResmiUrl = "https://.../sucveceza.jpg", Aciklama = "Bir başyapıt." },
                new Kitap { Ad = "Kürk Mantolu Madonna", Yazar = "Sabahattin Ali", Fiyat = 90, Kategori = "Roman", KapakResmiUrl = "https://.../kurkmantolu.jpg", Aciklama = "Türk edebiyatının önemli eserlerinden." }
            };
            context.Kitaplar.AddRange(kitaplar);
            context.SaveChanges();
        }
    }
} 