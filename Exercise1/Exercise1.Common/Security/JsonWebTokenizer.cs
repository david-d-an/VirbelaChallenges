using System;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

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

    }
}