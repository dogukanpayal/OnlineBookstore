namespace OnlineBookStore.API.DTOs
{
    /// <summary>
    /// Kullanıcı verisinin istemciye aktarımı için DTO.
    /// </summary>
    public class KullaniciDto
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
    }
} 