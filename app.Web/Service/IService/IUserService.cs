using app.Web.Models.DTO;

namespace app.Web.Service.IService
{
    public interface IUserService
    {
        Task<ResponseDTO> GetUsers();
        Task<ResponseDTO> DeleteUsers(string userId);
    }
}
