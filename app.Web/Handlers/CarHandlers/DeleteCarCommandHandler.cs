using app.Web.Commands.CarCommands;
using app.Web.Models.DTO;
using app.Web.Service.IService;
using MediatR;

namespace app.Web.Handlers.CarHandlers
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, ResponseDTO>
    {
        private readonly ICarService _carService;

        public DeleteCarCommandHandler(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<ResponseDTO> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDTO();

            response.Result = await _carService.DeleteCar(request.id);

            return response;
        }
    }
}
