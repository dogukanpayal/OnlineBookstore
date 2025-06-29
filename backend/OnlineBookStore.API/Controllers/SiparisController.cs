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
        private readonly IKitapService _kitapService;

        public SiparisController(ISiparisService siparisService, ISepetService sepetService, IKitapService kitapService, ILogger<SiparisController> logger)
        {
            _siparisService = siparisService;
            _sepetService = sepetService;
            _kitapService = kitapService;
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
            var kitaplar = (await _kitapService.GetAllAsync()).ToList();
            return Ok(siparisler.Select(s => SiparisMapper.ToDto(s, kitaplar)));
        }

        /// <summary>
        /// Admin için tüm siparişleri getirir.
        /// </summary>
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var siparisler = await _siparisService.GetAllAsync();
            var kitaplar = (await _kitapService.GetAllAsync()).ToList();
            var kullaniciService = (IKullaniciService)HttpContext.RequestServices.GetService(typeof(IKullaniciService));
            var kullanicilar = await kullaniciService.GetAllAsync();
            return Ok(siparisler.Select(s => SiparisMapper.ToDto(s, kitaplar, kullanicilar)));
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
                var kitaplar = (await _kitapService.GetAllAsync()).ToList();
                var kalemler = sepet.Select(s => {
                    var kitap = kitaplar.FirstOrDefault(k => k.Id == s.KitapId);
                    var fiyat = kitap != null ? kitap.Fiyat : 0m;
                    return (s.KitapId, s.Adet, fiyat);
                }).ToList();
                var siparis = await _siparisService.CreateAsync(kullaniciId, kalemler);
                await _sepetService.DeleteAllAsync(kullaniciId);
                return Ok(SiparisMapper.ToDto(siparis, kitaplar));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sipariş oluşturma hatası");
                return BadRequest(new { hata = ex.Message });
            }
        }

        /// <summary>
        /// Siparişi siler (Sadece Admin).
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _siparisService.DeleteAsync(id);
            return NoContent();
        }
    }
} 