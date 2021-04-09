using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Exercise1.Api.Authentication.Provider
{
    public class JwtMiddleware {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(
            RequestDelegate next, 
            IConfiguration configuration,
            ILogger<JwtMiddleware> logger)
        {
            this._next = next;
            this._configuration = configuration;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext context, IUserService userService) {
            var token = context.Request.Headers["Authorization"]
                        .FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                attachUserToContext(context, userService, token);

            await _next(context);
        }

        private void attachUserToContext(
            HttpContext context, 
            IUserService userService, 
            string token)
        {
            try {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire 
                    // exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                string userId = jwtToken.Claims.First(x => x.Type == "Userid").Value;

                // attach user to context on successful jwt validation
                // Must be blocking call to ensure attachment of User Identity and token
                context.Items["User"] = userService.GetByUserId(userId).Result;
            }
            catch(SecurityTokenExpiredException stex) {
                _logger.LogWarning(stex, stex.Message);
                // do nothing if jwt expired
                // user is not attached to context so request won't have access to secure routes
            }
            catch(Exception ex) {
                _logger.LogWarning(ex, ex.Message);
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}