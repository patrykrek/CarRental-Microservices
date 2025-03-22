using app.Services.AuthAPI.Models.DTO;

namespace app.Services.AuthAPI.Service.IService
{
    public interface IUserService
    {
        Task<ResponseDTO> GetUsers();
        Task<ResponseDTO> DeleteUser(string userId);
    }
}
