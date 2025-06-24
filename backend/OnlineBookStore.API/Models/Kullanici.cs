using System.ComponentModel.DataAnnotations;

namespace OnlineBookStore.API.Models
{
    /// <summary>
    /// Uygulama kullanıcısını temsil eder.
    /// </summary>
    public class Kullanici
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string AdSoyad { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string SifreHash { get; set; }

        [Required]
        public string Rol { get; set; } // "Admin" veya "Uye"
    }
} 