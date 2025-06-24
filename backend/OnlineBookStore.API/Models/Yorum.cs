using System.ComponentModel.DataAnnotations;

namespace OnlineBookStore.API.Models
{
    /// <summary>
    /// Kitap için kullanıcı yorumu.
    /// </summary>
    public class Yorum
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int KitapId { get; set; }

        [Required]
        public int KullaniciId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Icerik { get; set; }

        [Range(1,5)]
        public int Puan { get; set; }

        public DateTime Tarih { get; set; } = DateTime.Now;
    }
} 