using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.API.DTOs;
using OnlineBookStore.API.Mappers;
using OnlineBookStore.API.Services;
using System.Security.Claims;

namespace OnlineBookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SepetController : ControllerBase
    {
        private readonly ISepetService _sepetService;
        private readonly ILogger<SepetController> _logger;

        public SepetController(ISepetService sepetService, ILogger<SepetController> logger)
        {
            _sepetService = sepetService;
            _logger = logger;
        }

        /// <summary>
        /// Kullanıcının sepetini getirir.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var kullaniciId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var sepet = await _sepetService.GetByKullaniciIdAsync(kullaniciId);
            return Ok(sepet.Select(SepetItemMapper.ToDto));
        }

        /// <summary>
        /// Sepete kitap ekler veya adedini günceller.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromBody] SepetItemDto dto)
        {
            var kullaniciId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _sepetService.AddOrUpdateAsync(kullaniciId, dto.KitapId, dto.Adet);
            return Ok();
        }

        /// <summary>
        /// Sepetten kitap siler.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sepetService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Sepeti tamamen temizler.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            var kullaniciId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _sepetService.DeleteAllAsync(kullaniciId);
            return NoContent();
        }
    }
} 