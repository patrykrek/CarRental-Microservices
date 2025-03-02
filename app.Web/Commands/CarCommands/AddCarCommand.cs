using app.Web.Models.DTO;
using MediatR;

namespace app.Web.Commands.CarCommands
{
    public record AddCarCommand(AddCarDTO carDTO) : IRequest<ResponseDTO>;


}
