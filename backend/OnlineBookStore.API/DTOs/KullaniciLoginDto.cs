namespace OnlineBookStore.API.DTOs
{
    /// <summary>
    /// Kullanıcı giriş işlemi için DTO.
    /// </summary>
    public class KullaniciLoginDto
    {
        public string Email { get; set; }
        public string Sifre { get; set; }
    }
} 