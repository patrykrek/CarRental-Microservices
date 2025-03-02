using app.Web.Models.DTO;
using MediatR;

namespace app.Web.Queries.ReservationQueries
{
    public record GetAllReservationsQuery() : IRequest<ResponseDTO>;


}
