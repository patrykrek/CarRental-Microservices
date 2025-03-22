using app.Web.Models.DTO;
using MediatR;

namespace app.Web.Queries.UserQueries
{
    public record GetAllUsersQuery : IRequest<ResponseDTO>;
    
}
