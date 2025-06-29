using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineBookStore.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineBookStore.API.Services
{
    /// <summary>
    /// JWT token üretimi için servis.
    /// </summary>
    public interface ITokenService
    {
        string CreateToken(Kullanici kullanici);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;

        public TokenService(IConfiguration configuration, JwtSettings jwtSettings)
        {
            _configuration = configuration;
            _jwtSettings = jwtSettings;
        }

        public string CreateToken(Kullanici kullanici)
        {
            // LOG: Kullanılan JWT ayarlarını konsola yaz
            var key = _jwtSettings.Key;
            var issuer = _jwtSettings.Issuer;
            var audience = _jwtSettings.Audience;
            Console.WriteLine($"[TokenService] JWT Key: {key}");
            Console.WriteLine($"[TokenService] JWT Issuer: {issuer}");
            Console.WriteLine($"[TokenService] JWT Audience: {audience}");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, kullanici.Id.ToString()),
                new Claim(ClaimTypes.Name, kullanici.AdSoyad),
                new Claim(ClaimTypes.Email, kullanici.Email),
                new Claim(ClaimTypes.Role, kullanici.Rol)
            };

            var keyBytes = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(keyBytes, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
} 