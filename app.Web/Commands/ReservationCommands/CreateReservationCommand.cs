using app.Web.Models.DTO;
using app.Web.Models.ViewModel;
using MediatR;

namespace app.Web.Commands.ReservationCommands
{
    public record CreateReservationCommand(ReservationCarViewModel reservation) : IRequest<ResponseDTO>;
    
    
}
