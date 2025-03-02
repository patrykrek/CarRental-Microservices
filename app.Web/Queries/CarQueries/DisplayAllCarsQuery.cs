using app.Web.Models.DTO;
using MediatR;

namespace app.Web.Queries.CarQueries
{
    public record DisplayAllCarsQuery : IRequest<ResponseDTO>;


}
