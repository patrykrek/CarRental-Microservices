using app.Services.ReservationAPI.Models;
using app.Services.ReservationAPI.Models.DTO;

namespace app.Services.ReservationAPI.Repository.Interface
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetAllReservations();
        Task<List<Reservation>> GetUserReservations(string userId);
        Task AddReservation(Reservation reservation);
        
    }
}
