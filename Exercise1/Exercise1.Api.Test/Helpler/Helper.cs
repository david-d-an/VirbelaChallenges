using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using Exercise1.Data.Models.VirbelaListing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Exercise1.Api.Test.Helper {
    public static class Helper
    {
        public static IConfigurationRoot GetConfiguration() {
            string appsettingDirectory = 
                Directory
                .GetCurrentDirectory()
                .Replace(".Test/bin/Debug/netcoreapp3.1", "");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(appsettingDirectory)
                .AddJsonFile("appsettings.Development.json")
                .Build();
            return configuration;
        }

        public static ControllerContext GetControllerContext(Listinguser user) {
            var context = new DefaultHttpContext();
            context.Items["User"] = user;
            context.Request.Headers["Authorization"] = GenerateJwtToken(user);
            return new ControllerContext() {
                HttpContext = context
            };
        }

        public static string HashPassword(string password) {
            #nullable enable
            return new PasswordHasher<object?>()
                .HashPassword(null, password);
            #nullable disable

        }

        private static string GenerateJwtToken(Listinguser user) {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var config = GetConfiguration();
            var key = Encoding.ASCII.GetBytes(config["Jwt:Key"]);
            // Default token expiration 30 minutes
            DateTime expiry = DateTime.Now.AddMinutes(30);
            if (double.TryParse(config["Jwt:LifeSpan"],
                out double tokenLife))
            {
                expiry = DateTime.UtcNow.AddMinutes(tokenLife);
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim("Id", user.Id.ToString()),
                new Claim("Userid", user.Userid),
                new Claim("Firstname", user.Firstname),
                new Claim("Lastname", user.Lastname),
                new Claim("RegionId", user.RegionId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                Expires = expiry,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = config["Jwt:Issuer"],
                Audience = config["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}