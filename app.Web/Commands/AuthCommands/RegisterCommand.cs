using app.Web.Models.DTO;
using MediatR;

namespace app.Web.Commands.AuthCommands
{
    public record RegisterCommand(RegistrationRequestDTO RequestDTO) : IRequest<ResponseDTO>;


}
