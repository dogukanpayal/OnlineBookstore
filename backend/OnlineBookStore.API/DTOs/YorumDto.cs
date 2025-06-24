namespace OnlineBookStore.API.DTOs
{
    /// <summary>
    /// Yorum verisinin istemciye aktarımı için DTO.
    /// </summary>
    public class YorumDto
    {
        public int Id { get; set; }
        public int KitapId { get; set; }
        public int KullaniciId { get; set; }
        public string Icerik { get; set; }
        public int Puan { get; set; }
        public DateTime Tarih { get; set; }
    }
} 