using app.Web.Models.DTO;
using MediatR;

namespace app.Web.Commands.AuthCommands
{
    public record LoginCommand(LoginRequestDTO LoginRequestDTO) : IRequest<ResponseDTO>;



}
