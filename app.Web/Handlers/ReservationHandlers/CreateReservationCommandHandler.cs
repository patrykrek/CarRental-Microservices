using app.Web.Commands.ReservationCommands;
using app.Web.Models.DTO;
using app.Web.Service.IService;
using MediatR;

namespace app.Web.Handlers.ReservationHandlers
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, ResponseDTO>
    {
        private readonly IReservationService _reservationService;

        public CreateReservationCommandHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<ResponseDTO?> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            return await _reservationService.CreateReservation(new AddReservationDTO
            {
                StartDate = request.reservation.StartDate,
                EndDate = request.reservation.EndDate,
                CarId = request.reservation.carDTO.Id,
                UserId = request.reservation.UserId
            });
        }
    }
}
