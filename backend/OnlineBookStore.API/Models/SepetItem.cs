using System.ComponentModel.DataAnnotations;

namespace OnlineBookStore.API.Models
{
    /// <summary>
    /// Sepetteki bir kitap.
    /// </summary>
    public class SepetItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int KullaniciId { get; set; }

        [Required]
        public int KitapId { get; set; }

        [Required]
        [Range(1, 100)]
        public int Adet { get; set; }
    }
} 