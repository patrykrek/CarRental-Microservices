using app.Web.Models.DTO;

namespace app.Web.Service.IService
{
    public interface IReservationService
    {
        Task<ResponseDTO?> GetAllReservations();
        Task<ResponseDTO?> GetUserReservations(string userId);
        Task<ResponseDTO?> CreateReservation(AddReservationDTO reservation);
    }
}
