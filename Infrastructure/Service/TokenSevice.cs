using Application.LDAP;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class TokenService:ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiryMinutes;
        private readonly IMemoryCache _cache;

       // private readonly HashSet<string> _blackllistToken= new HashSet<string>();

        public TokenService(IConfiguration configuration , IMemoryCache memoryCache)
        {
            _configuration = configuration;
             _cache = memoryCache;

            // استخراج إعدادات التوكن من ملف التكوين
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is missing")));

            _issuer = _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer is missing");
            _audience = _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience is missing");
            _expiryMinutes = int.TryParse(_configuration["Jwt:ExpiryMinutes"], out int minutes) ? minutes : 60;
        }
        
        public string GenerateToken(User user)
        {
            // 1.   (Claims) 
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim("department", user.Department),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            // 2.  (Credentials)
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            // 3. وصف التوكن
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = DateTime.UtcNow.AddMinutes(_expiryMinutes),
            //    SigningCredentials = creds,
            //    Issuer = _issuer,
            //    Audience = _audience
            //};
            var tokenDescriptor = new JwtSecurityToken(
                claims: claims,
                issuer:_issuer,
                audience:_audience,
                expires: DateTime.UtcNow.AddMinutes(_expiryMinutes),
                signingCredentials:creds

                );
            
            // 4. إنشاء التوكن
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            // 5. إرجاع التوكن كسلسلة
            return token;
        }

        public void AddToBlackListToken(string token)
        {

            var expiration = TimeSpan.Parse(_configuration["Jwt:Expiration"] ?? "01:00:00");
            _cache.Set(token, "blacklisted", new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            });

           
        }
      

        public bool IsBlackListToken(string token)
        {
            return _cache.TryGetValue(token, out _);
        }




    }


}
 