using System.ComponentModel.DataAnnotations;

namespace OnlineBookStore.API.Models
{
    /// <summary>
    /// Kitap modelini temsil eder.
    /// </summary>
    public class Kitap
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Kitap adı zorunludur.")]
        [MaxLength(100)]
        public string Ad { get; set; }

        [Required]
        public string Yazar { get; set; }

        [Required]
        public decimal Fiyat { get; set; }

        public string Aciklama { get; set; }

        public string Kategori { get; set; }

        public string KapakResmiUrl { get; set; }
    }
} 