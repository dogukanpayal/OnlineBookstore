using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.API.DTOs;
using OnlineBookStore.API.Models;
using OnlineBookStore.API.Data;
using System.Linq;
using System.Security.Claims;

namespace OnlineBookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KullaniciController : ControllerBase
    {
        private readonly AppDbContext _context;
        public KullaniciController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Giriş yapan kullanıcının profilini döndürür.
        /// </summary>
        [HttpGet("profil")]
        [Authorize]
        public IActionResult Profil()
        {
            var kullaniciIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(kullaniciIdStr))
                return Unauthorized();
            if (!int.TryParse(kullaniciIdStr, out int kullaniciId))
                return Unauthorized();

            var kullanici = _context.Kullanicilar.FirstOrDefault(k => k.Id == kullaniciId);
            if (kullanici == null)
                return NotFound();

            var dto = new KullaniciDto
            {
                Id = kullanici.Id,
                AdSoyad = kullanici.AdSoyad,
                Email = kullanici.Email,
                Rol = kullanici.Rol
            };
            return Ok(dto);
        }

        /// <summary>
        /// Giriş yapan kullanıcının profilini günceller.
        /// </summary>
        [HttpPut("profil")]
        [Authorize]
        public IActionResult ProfilGuncelle([FromBody] KullaniciDto dto)
        {
            var kullaniciIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(kullaniciIdStr))
                return Unauthorized();
            if (!int.TryParse(kullaniciIdStr, out int kullaniciId))
                return Unauthorized();

            var kullanici = _context.Kullanicilar.FirstOrDefault(k => k.Id == kullaniciId);
            if (kullanici == null)
                return NotFound();

            // Güncellenebilir alanlar
            kullanici.AdSoyad = dto.AdSoyad;
            kullanici.Email = dto.Email;
            // Diğer alanlar gerekiyorsa ekle

            _context.SaveChanges();

            var updatedDto = new KullaniciDto
            {
                Id = kullanici.Id,
                AdSoyad = kullanici.AdSoyad,
                Email = kullanici.Email,
                Rol = kullanici.Rol
            };
            return Ok(updatedDto);
        }

        /// <summary>
        /// Giriş yapan kullanıcının hesabını siler.
        /// </summary>
        [HttpDelete("profil")]
        [Authorize]
        public IActionResult HesabiSil()
        {
            var kullaniciIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(kullaniciIdStr))
                return Unauthorized();
            if (!int.TryParse(kullaniciIdStr, out int kullaniciId))
                return Unauthorized();

            var kullanici = _context.Kullanicilar.FirstOrDefault(k => k.Id == kullaniciId);
            if (kullanici == null)
                return NotFound();

            _context.Kullanicilar.Remove(kullanici);
            _context.SaveChanges();

            return NoContent();
        }
    }
} 