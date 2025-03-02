using app.Web.Models.DTO;
using app.Web.Queries.ReservationQueries;
using app.Web.Service.IService;
using MediatR;

namespace app.Web.Handlers.ReservationHandlers
{
    public class GetUserReservationsQueryHandler : IRequestHandler<GetUserReservationsQuery, ResponseDTO>
    {
        private readonly IReservationService _reservationService;

        public GetUserReservationsQueryHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<ResponseDTO> Handle(GetUserReservationsQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDTO();

            response = await _reservationService.GetUserReservations(request.userId);

            return response;
        }
    }
}
