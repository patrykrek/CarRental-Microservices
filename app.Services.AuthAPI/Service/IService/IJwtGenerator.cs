using app.Services.AuthAPI.Models;

namespace app.Services.AuthAPI.Service.IService
{
    public interface IJwtGenerator
    {
        string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
