using System;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Exercise1.Common.Security {
    public static class JsonWebTokenizer {
        public static string GenerateToken(
            Claim[] claims,
            string key,
            string issuer,
            string audience,
            string algorithm = SecurityAlgorithms.HmacSha256)    
        {    
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));    
            var credentials = new SigningCredentials(securityKey, algorithm);    

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static bool IsTokenExpired(JwtSecurityToken jwtSecurityToken) {
            var expiry = jwtSecurityToken.Claims
                        .FirstOrDefault(c => c.Type == "exp").Value;
            if (double.TryParse(expiry, out double exp)) {
                return DateTime.UtcNow >= ExpirtyToDateTime(exp);
            }
            return true;
        }

        public static DateTime ExpirtyToDateTime(double exp) {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime expiryDateTime = epoch.AddSeconds(exp);
            return expiryDateTime;
         }

    }
}