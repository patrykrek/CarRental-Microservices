using app.Web.Models.DTO;
using app.Web.Queries.ReservationQueries;
using app.Web.Service.IService;
using MediatR;

namespace app.Web.Handlers.ReservationHandlers
{
    public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, ResponseDTO>
    {
        private readonly IReservationService _reservationService;

        public GetAllReservationsQueryHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<ResponseDTO> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDTO();

            response = await _reservationService.GetAllReservations();

            return response;
        }
    }
}
