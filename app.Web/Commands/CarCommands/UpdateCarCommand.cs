using app.Web.Models.DTO;
using MediatR;

namespace app.Web.Commands.CarCommands
{
    public record UpdateCarCommand(UpdateCarDTO carDTO) : IRequest<ResponseDTO>;
    
    
}
