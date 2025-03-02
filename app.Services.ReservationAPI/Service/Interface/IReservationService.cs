using app.Services.ReservationAPI.Models.DTO;

namespace app.Services.ReservationAPI.Service.Interface
{
    public interface IReservationService
    {
        Task<ResponseDTO> GetAllReservations();
        Task<ResponseDTO> GetUserReservations(string userId);
        Task<ResponseDTO> CreateReservation(AddReservationDTO reservationDTO);
    }
}
