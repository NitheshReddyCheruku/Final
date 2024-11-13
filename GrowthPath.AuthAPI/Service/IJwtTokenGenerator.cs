
using GrowthPath.AuthAPI.Models;

namespace GrowthPath.AuthAPI.Service
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(ApplicationUser applicationUser);
    }
}
