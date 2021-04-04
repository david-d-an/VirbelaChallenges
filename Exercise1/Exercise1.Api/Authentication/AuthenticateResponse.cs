using Exercise1.Data.Models.VirbelaListing;

namespace Exercise1.Api.Authentication
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Userid { get; set; }
        public string Email { get; set; }
        public int RegionId { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(Listinguser user, string token)
        {
            Id = user.Id;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Userid = user.Userid;
            Email = user.Email;
            RegionId = user.RegionId;
            Token = token;
        }
    }
}