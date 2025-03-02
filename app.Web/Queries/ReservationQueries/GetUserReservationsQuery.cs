using app.Web.Models.DTO;
using MediatR;

namespace app.Web.Queries.ReservationQueries
{
    public record GetUserReservationsQuery(string userId) : IRequest<ResponseDTO>;


}
