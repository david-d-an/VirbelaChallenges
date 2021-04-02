using Exercise1.Data.Models.VirbelaListing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;

namespace Exercise1.Api.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TokenAuthorizeAttribute : Attribute, IAuthorizationFilter {
        private bool isTokenExpired(string token) {             
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = tokenHandler.ReadJwtToken(token);
            var expiry = jwtSecurityToken.Claims
                        .FirstOrDefault(c => c.Type == "exp").Value;

            if (double.TryParse(expiry, out double exp)) {
                return DateTime.UtcNow >= ExpirtyToDateTime(exp);
            }
            return true;
         }

         private DateTime ExpirtyToDateTime(double exp) {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime expiryDateTime = epoch.AddSeconds(exp);
            return expiryDateTime;
         }

        public void OnAuthorization(AuthorizationFilterContext context) {
            var user = (Listinguser)context.HttpContext.Items["User"];
            var token = context.HttpContext.Request.Headers["Authorization"]
                        .FirstOrDefault()?.Split(" ").Last();

            if (user == null || token == null || isTokenExpired(token)) {
                // not logged in
                context.Result = new JsonResult(new { 
                    message = "Unauthorized" 
                }) {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}
