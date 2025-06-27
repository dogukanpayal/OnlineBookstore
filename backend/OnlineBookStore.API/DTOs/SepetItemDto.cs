namespace OnlineBookStore.API.DTOs
{
    /// <summary>
    /// Sepet verisinin istemciye aktarımı için DTO.
    /// </summary>
    public class SepetItemDto
    {
        public int Id { get; set; }
        public int KitapId { get; set; }
        public int Adet { get; set; }
        public KitapDto? Kitap { get; set; }
    }
} 