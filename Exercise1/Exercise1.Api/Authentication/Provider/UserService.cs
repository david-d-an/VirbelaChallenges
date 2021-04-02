using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Exercise1.Data.Models.Authentication;
using Exercise1.Data.Models.VirbelaListing;
using Exercise1.Data.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Exercise1.Api.Authentication.Provider
{
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        // private List<Listinguser> _mockListingUsers = new List<Listinguser> {
        //     new Listinguser { 
        //         Id = 1, 
        //         Userid = "jsmith",
        //         Email = "jsmith@contoso.com",
        //         Firstname = "John",
        //         Lastname = "Smith",
        //         Password = "AQAAAAEAACcQAAAAEDxkjJUF+sOyIinGOU2cO0YfvDtEP0hbK526b/qF/USWtT1fVwDVWf6yBKhAir9fCQ==",
        //         RegionId = 5
        //     }
        // };

        private IConfiguration _configuration;
        private IUnitOfWork _unitOfWork;

        public UserService(
            IConfiguration configuration,
            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthenticateResponse> Authenticate(LoginModel login)
        {
            // await Task.Delay(0);
            // var user = _mockListingUsers
            //     .SingleOrDefault(x => x.Userid == login.Userid);
            var user = await GetByUserId(login.Userid);
            if (user == null)
                return null;

            #nullable enable
            var hashedPassword = user.Password;
            var passwordVerificationResult = 
                new PasswordHasher<object?>()
                .VerifyHashedPassword(null, hashedPassword, login.Password);
            #nullable disable
            if (passwordVerificationResult != PasswordVerificationResult.Success)
                return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);
            user.Password = null;
            return new AuthenticateResponse(user, token);
        }

        public async Task<IEnumerable<Listinguser>> GetAll()
        {
            // return _mockListingUsers;
            return await _unitOfWork
                .ListinguserRepository
                .GetAsync();
        }

        public async Task<Listinguser> GetByUserId(string userid)
        {
            // await Task.Delay(0);
            // return _mockListingUsers
            //     .SingleOrDefault(x => x.Userid == userid);
            return await _unitOfWork
                .ListinguserRepository
                .GetAsync(userid);
        }

        // helper methods
        private string generateJwtToken(Listinguser user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Userid", user.Userid),
                    new Claim("Firstname", user.Firstname),
                    new Claim("Lastname", user.Lastname),
                    new Claim("RegionId", user.RegionId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                // Expiriation shall be short. Kepping 30 mins for ease of debug
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}