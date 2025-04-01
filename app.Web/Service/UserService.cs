using app.Web.Models.DTO;
using app.Web.Service.IService;
using app.Web.Utility;

namespace app.Web.Service
{
    public class UserService : IUserService
    {
        private readonly IBaseService _baseService;

        public UserService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> DeleteUsers(string userId)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ApiGatewayBase + "/api/users/" + userId
            });
        }

        public async Task<ResponseDTO?> GetUsers()
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ApiGatewayBase + "/api/users"
            });
        }
    }
}
