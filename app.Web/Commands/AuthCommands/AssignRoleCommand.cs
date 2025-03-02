using app.Web.Models.DTO;
using MediatR;

namespace app.Web.Commands.AuthCommands
{
    public record AssignRoleCommand(RegistrationRequestDTO request) : IRequest<ResponseDTO>;


}
