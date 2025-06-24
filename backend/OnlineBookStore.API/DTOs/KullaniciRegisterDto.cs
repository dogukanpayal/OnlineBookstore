namespace OnlineBookStore.API.DTOs
{
    /// <summary>
    /// Kullanıcı kayıt işlemi için DTO.
    /// </summary>
    public class KullaniciRegisterDto
    {
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
    }
} 