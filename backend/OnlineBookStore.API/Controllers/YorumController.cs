using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.API.DTOs;
using OnlineBookStore.API.Mappers;
using OnlineBookStore.API.Models;
using OnlineBookStore.API.Services;
using System.Security.Claims;

namespace OnlineBookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YorumController : ControllerBase
    {
        private readonly IYorumService _yorumService;
        private readonly ILogger<YorumController> _logger;

        public YorumController(IYorumService yorumService, ILogger<YorumController> logger)
        {
            _yorumService = yorumService;
            _logger = logger;
        }

        /// <summary>
        /// Bir kitaba ait tüm yorumları getirir.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("kitap/{kitapId}")]
        public async Task<IActionResult> GetByKitapId(int kitapId)
        {
            var yorumlar = await _yorumService.GetByKitapIdAsync(kitapId);
            return Ok(yorumlar.Select(YorumMapper.ToDto));
        }

        /// <summary>
        /// Yorum ekler (sadece giriş yapmış kullanıcılar).
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] YorumDto dto)
        {
            try
            {
                var kullaniciIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(kullaniciIdStr))
                    return Unauthorized();
                
                var kullaniciId = int.Parse(kullaniciIdStr);
                var yorum = YorumMapper.ToModel(dto);
                yorum.KullaniciId = kullaniciId;
                yorum.Tarih = DateTime.Now;
                await _yorumService.AddAsync(yorum);
                return Ok(YorumMapper.ToDto(yorum));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yorum ekleme hatası");
                return BadRequest(new { hata = ex.Message });
            }
        }

        /// <summary>
        /// Yorumu siler (sadece yorumu ekleyen veya admin).
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var yorum = await _yorumService.GetByIdAsync(id);
                var kullaniciIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(kullaniciIdStr))
                    return Unauthorized();
                
                var kullaniciId = int.Parse(kullaniciIdStr);
                var rol = User.FindFirstValue(ClaimTypes.Role);
                if (yorum == null || (yorum.KullaniciId != kullaniciId && rol != "Admin"))
                    return Forbid();
                await _yorumService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yorum silme hatası");
                return BadRequest(new { hata = ex.Message });
            }
        }
    }
} 