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
    [Authorize]
    public class SiparisController : ControllerBase
    {
        private readonly ISiparisService _siparisService;
        private readonly ISepetService _sepetService;
        private readonly ILogger<SiparisController> _logger;

        public SiparisController(ISiparisService siparisService, ISepetService sepetService, ILogger<SiparisController> logger)
        {
            _siparisService = siparisService;
            _sepetService = sepetService;
            _logger = logger;
        }

        /// <summary>
        /// Kullanıcının tüm siparişlerini getirir.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var kullaniciId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var siparisler = await _siparisService.GetByKullaniciIdAsync(kullaniciId);
            return Ok(siparisler.Select(SiparisMapper.ToDto));
        }

        /// <summary>
        /// Admin için tüm siparişleri getirir.
        /// </summary>
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var siparisler = await _siparisService.GetAllAsync();
            return Ok(siparisler.Select(SiparisMapper.ToDto));
        }

        /// <summary>
        /// Sepeti siparişe çevirir.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            try
            {
                var kullaniciId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var sepet = await _sepetService.GetByKullaniciIdAsync(kullaniciId);
                if (!sepet.Any())
                    return BadRequest(new { hata = "Sepetiniz boş." });
                var kalemler = sepet.Select(s => (s.KitapId, s.Adet, 0m)).ToList(); // Fiyatı kitap tablosundan çekmek için güncellenebilir
                var siparis = await _siparisService.CreateAsync(kullaniciId, kalemler);
                await _sepetService.DeleteAllAsync(kullaniciId);
                return Ok(SiparisMapper.ToDto(siparis));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sipariş oluşturma hatası");
                return BadRequest(new { hata = ex.Message });
            }
        }
    }
} 