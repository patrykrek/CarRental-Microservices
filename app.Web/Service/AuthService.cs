using app.Web.Models.DTO;
using app.Web.Service.IService;
using app.Web.Utility;

namespace app.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> AssignRole(RegistrationRequestDTO registrationRequestDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDTO,
                Url = SD.ApiGatewayBase + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponseDTO?> Login(LoginRequestDTO loginRequestDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDTO,               
                Url = SD.ApiGatewayBase + "/api/auth/login"
            }, withBearer: false);
        }

        public async Task<ResponseDTO?> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDTO,
                Url = SD.ApiGatewayBase + "/api/auth/register"
            }, withBearer: false);
        }
    }
}
