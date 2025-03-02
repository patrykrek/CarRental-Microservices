using app.Web.Models.DTO;

namespace app.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDTO?> Register(RegistrationRequestDTO registrationRequestDTO);
        Task<ResponseDTO?> Login(LoginRequestDTO loginRequestDTO);
        Task<ResponseDTO?> AssignRole(RegistrationRequestDTO registrationRequestDTO);
    }
}
