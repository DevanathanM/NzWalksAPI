using Microsoft.AspNetCore.Identity;

namespace NZWalks.api.Repository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> Roles );
    }
}
