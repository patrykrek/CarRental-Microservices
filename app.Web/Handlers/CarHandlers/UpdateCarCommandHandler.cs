using app.Web.Commands.CarCommands;
using app.Web.Models.DTO;
using app.Web.Service.IService;
using MediatR;

namespace app.Web.Handlers.CarHandlers
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, ResponseDTO>
    {
        private readonly ICarService _carService;

        public UpdateCarCommandHandler(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<ResponseDTO> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDTO();

            response.Result = await _carService.UpdateCar(request.carDTO);

            return response;
        }
    }
}
