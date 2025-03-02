using app.Services.ReservationAPI.Models.DTO;

namespace app.Services.ReservationAPI.Service.Interface
{
    public interface IBaseService
    {
        Task<ResponseDTO> SendAsync(RequestDTO request, bool withBearer = true);
    }
}
