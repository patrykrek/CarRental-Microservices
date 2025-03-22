using app.Web.Models.DTO;
using MediatR;

namespace app.Web.Commands.UserCommands
{
    public record DeleteUserCommand(string Id) : IRequest<ResponseDTO>;
    
    
}
