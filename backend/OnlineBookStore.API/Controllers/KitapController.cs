using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.API.DTOs;
using OnlineBookStore.API.Mappers;
using OnlineBookStore.API.Services;

namespace OnlineBookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KitapController : ControllerBase
    {
        private readonly IKitapService _kitapService;
        private readonly ILogger<KitapController> _logger;

        public KitapController(IKitapService kitapService, ILogger<KitapController> logger)
        {
            _kitapService = kitapService;
            _logger = logger;
        }

        /// <summary>
        /// Tüm kitapları getirir.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var kitaplar = await _kitapService.GetAllAsync();
            var kitapDtos = kitaplar.Select(KitapMapper.ToDto);
            return Ok(kitapDtos);
        }

        /// <summary>
        /// Yeni kitap ekler (Sadece Admin).
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] KitapDto dto)
        {
            try
            {
                var kitap = KitapMapper.ToModel(dto);
                await _kitapService.AddAsync(kitap);
                return Ok(KitapMapper.ToDto(kitap));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kitap ekleme hatası");
                return BadRequest(new { hata = ex.Message });
            }
        }

        /// <summary>
        /// Kitap siler (Sadece Admin).
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _kitapService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kitap silme hatası");
                return BadRequest(new { hata = ex.Message });
            }
        }

        /// <summary>
        /// Kitap detayını getirir (Tüm roller erişebilir).
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var kitap = await _kitapService.GetByIdAsync(id);
            if (kitap == null)
                return NotFound();
            return Ok(KitapMapper.ToDto(kitap));
        }

        /// <summary>
        /// Kitap arama, filtreleme, sayfalama ve sıralama (herkes erişebilir).
        /// </summary>
        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] string? arama, [FromQuery] string? kategori, [FromQuery] int sayfa = 1, [FromQuery] int sayfaBoyutu = 10, [FromQuery] string? sirala = null)
        {
            var (kitaplar, toplamKayit) = await _kitapService.SearchAsync(arama, kategori, sayfa, sayfaBoyutu, sirala);
            return Ok(new { kitaplar = kitaplar.Select(KitapMapper.ToDto), toplamKayit });
        }

        // Diğer CRUD işlemleri, arama, filtreleme, detay vs. eklenebilir
    }
} 