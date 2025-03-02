using app.Web.Models.DTO;
using MediatR;

namespace app.Web.Queries.CarQueries
{
    public record GetCarByIdQuery(int id) : IRequest<ResponseDTO>;


}
