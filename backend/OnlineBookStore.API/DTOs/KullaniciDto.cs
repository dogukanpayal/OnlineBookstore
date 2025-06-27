namespace OnlineBookStore.API.DTOs
{
    /// <summary>
    /// Kullanıcı verisinin istemciye aktarımı için DTO.
    /// Frontend ile birebir uyumlu olarak AdSoyad ve Rol alanları kullanılır.
    /// Rol alanı profil güncellemede zorunlu değildir.
    /// </summary>
    public class KullaniciDto
    {
        public int? Id { get; set; }
        public string AdSoyad { get; set; } // Kullanıcının tam adı (Ad + Soyad)
        public string Email { get; set; }   // Kullanıcının e-posta adresi
        public string? Rol { get; set; }    // Kullanıcının rolü (örn: Admin, Kullanici) - opsiyonel
    }
} 