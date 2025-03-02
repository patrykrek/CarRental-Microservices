using app.Web.Models.DTO;
using app.Web.Service.IService;
using app.Web.Utility;

namespace app.Web.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IBaseService _baseService;
        public ReservationService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> CreateReservation(AddReservationDTO reservation)
        {
             return await _baseService.SendAsync(new RequestDTO()
             {
                 ApiType = SD.ApiType.POST,
                 Data = reservation,
                 Url = SD.ReservationApiBase + "/api/reservation/create"
             });
        }

        public async Task<ResponseDTO?> GetAllReservations()
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ReservationApiBase + "/api/reservation"
            });
        }

        public async Task<ResponseDTO?> GetUserReservations(string userId)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ReservationApiBase + "/api/reservation/" + userId
            });
        }
    }
}
