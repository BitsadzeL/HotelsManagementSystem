using Microsoft.AspNetCore.Identity;

namespace Hotels.Service.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IdentityUser<int> applicationUser, IEnumerable<string> roles);
    }
}
