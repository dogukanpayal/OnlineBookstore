using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.API.DTOs;
using OnlineBookStore.API.Mappers;
using OnlineBookStore.API.Services;
using System.Security.Claims;
using OnlineBookStore.API.Models;

namespace OnlineBookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SepetController : ControllerBase
    {
        private readonly ISepetService _sepetService;
        private readonly ILogger<SepetController> _logger;
        private readonly IKitapService _kitapService;

        public SepetController(ISepetService sepetService, ILogger<SepetController> logger, IKitapService kitapService)
        {
            _sepetService = sepetService;
            _logger = logger;
            _kitapService = kitapService;
        }

        /// <summary>
        /// Kullanıcının sepetini getirir.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var kullaniciIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(kullaniciIdStr))
                return Unauthorized();
            
            var kullaniciId = int.Parse(kullaniciIdStr);
            var sepet = await _sepetService.GetByKullaniciIdAsync(kullaniciId);
            var kitapIdler = sepet.Select(s => s.KitapId).Distinct().ToList();
            var kitaplar = new Dictionary<int, Kitap>();
            foreach (var item in sepet)
            {
                if (!kitaplar.ContainsKey(item.KitapId))
                {
                    var kitap = await _kitapService.GetByIdAsync(item.KitapId);
                    if (kitap != null)
                        kitaplar[item.KitapId] = kitap;
                }
            }
            var dtoList = sepet.Select(item => SepetItemMapper.ToDto(item, kitaplar.ContainsKey(item.KitapId) ? kitaplar[item.KitapId] : null));
            return Ok(dtoList);
        }

        /// <summary>
        /// Sepete kitap ekler veya adedini günceller.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromBody] SepetItemDto dto)
        {
            var kullaniciIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(kullaniciIdStr))
                return Unauthorized();
            
            var kullaniciId = int.Parse(kullaniciIdStr);
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
            var kullaniciIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(kullaniciIdStr))
                return Unauthorized();
            
            var kullaniciId = int.Parse(kullaniciIdStr);
            await _sepetService.DeleteAllAsync(kullaniciId);
            return NoContent();
        }
    }
} 