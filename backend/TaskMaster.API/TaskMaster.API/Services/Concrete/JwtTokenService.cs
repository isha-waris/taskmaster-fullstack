using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskMaster.API.Entities;
using TaskMaster.API.Services.Interfaces;

namespace TaskMaster.API.Services.Concrete
{
    public class JwtTokenService:IJwtTokenService
    {
        private readonly IConfiguration _config;
        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateToken(User user)
        {
            // 1️⃣ Claims = identity info
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName)
                };

            // 2️⃣ Key
            var jwtKey = _config["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT key is not configured.");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            // 3️⃣ Credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 4️⃣ Token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            // 5️⃣ Return token string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
