using GustoGlide.Services.AuthAPI.Models;

namespace GustoGlide.Services.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
