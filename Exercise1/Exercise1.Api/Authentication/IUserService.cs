using System.Collections.Generic;
using System.Threading.Tasks;
using Exercise1.Data.Models.Authentication;
using Exercise1.Data.Models.VirbelaListing;

namespace Exercise1.Api.Authentication
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(LoginModel model);
        Task<IEnumerable<Listinguser>> GetAll();
        Task<Listinguser> GetByUserId(string id);
    }
}