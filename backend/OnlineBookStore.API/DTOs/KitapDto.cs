namespace OnlineBookStore.API.DTOs
{
    /// <summary>
    /// Kitap verisinin istemciye aktarımı için kullanılan DTO.
    /// </summary>
    public class KitapDto
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Yazar { get; set; }
        public decimal Fiyat { get; set; }
        public string Kategori { get; set; }
        public string KapakResmiUrl { get; set; }
        public string Aciklama { get; set; }
    }
} 