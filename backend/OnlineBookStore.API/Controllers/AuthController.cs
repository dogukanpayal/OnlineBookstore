using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.API.DTOs;
using OnlineBookStore.API.Mappers;
using OnlineBookStore.API.Services;

namespace OnlineBookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IKullaniciService _kullaniciService;
        private readonly ILogger<AuthController> _logger;
        private readonly TokenService _tokenService;

        public AuthController(IKullaniciService kullaniciService, ILogger<AuthController> logger, TokenService tokenService)
        {
            _kullaniciService = kullaniciService;
            _logger = logger;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Kullanıcı kaydı
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] KullaniciRegisterDto dto)
        {
            try
            {
                var kullanici = await _kullaniciService.RegisterAsync(dto.AdSoyad, dto.Email, dto.Sifre);
                return Ok(KullaniciMapper.ToDto(kullanici));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Kayıt hatası");
                return BadRequest(new { hata = ex.Message });
            }
        }

        /// <summary>
        /// Kullanıcı girişi
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] KullaniciLoginDto dto)
        {
            try
            {
                var kullanici = await _kullaniciService.LoginAsync(dto.Email, dto.Sifre);
                var token = _tokenService.GenerateToken(kullanici);
                return Ok(new { kullanici = KullaniciMapper.ToDto(kullanici), token });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Giriş hatası");
                return BadRequest(new { hata = ex.Message });
            }
        }
    }
} 