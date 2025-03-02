using app.Services.ReservationAPI.Models.DTO;

namespace app.Services.ReservationAPI.Service.Interface
{
    public interface ICarService
    {
        Task<ResponseDTO> GetCarPrice(int carId);
        Task<ResponseDTO> GetCarById(int carId);
    }
}
