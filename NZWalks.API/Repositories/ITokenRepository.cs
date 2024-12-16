using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
    public interface ITokenRepository
    {
        //IdentityUser comes from Microsoft.AspNetCore.Identity
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
