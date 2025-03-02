using app.Web.Models.DTO;
using MediatR;

namespace app.Web.Commands.CarCommands
{
    public record DeleteCarCommand(int id) : IRequest<ResponseDTO>;


}
