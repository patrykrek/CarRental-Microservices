

using app.Services.AuthAPI.Models;

namespace app.Services.AuthAPI.Repository.Interface
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetAllUsers();
        Task<ApplicationUser> FindUser(string userId);
        Task DeleteUser(ApplicationUser user);
    }
}
