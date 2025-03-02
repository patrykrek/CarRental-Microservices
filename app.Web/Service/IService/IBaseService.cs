using app.Web.Models.DTO;

namespace app.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDTO?> SendAsync(RequestDTO requestDTO, bool withBearer = true);
    }
}
