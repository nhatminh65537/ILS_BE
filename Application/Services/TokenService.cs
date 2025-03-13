using ILS_BE.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ILS_BE.Application.Services
{
    public class TokenService : ITokenService
    {
        IConfiguration _config;
        private readonly IDistributedCache _cache;

        public TokenService(IConfiguration configuration, IDistributedCache cache)
        {
            _config = configuration;
            _cache = cache;
        }

        public string GenerateJwtToken(int userId)
        {
            var secretKey = Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]!);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(secretKey);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task RevokeTokenAsync(string jti)
        {
            await _cache.SetStringAsync($"revoked:{jti}", "revoked");
        }

        public async Task<bool> IsTokenRevokedAsync(string jti)
        {
            var revoked = await _cache.GetStringAsync($"revoked:{jti}");
            return revoked != null;
        }
    }
}
