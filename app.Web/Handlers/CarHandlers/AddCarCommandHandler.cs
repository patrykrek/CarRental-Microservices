using app.Web.Commands.CarCommands;
using app.Web.Models.DTO;
using app.Web.Service.IService;
using MediatR;

namespace app.Web.Handlers.CarHandlers
{
    public class AddCarCommandHandler : IRequestHandler<AddCarCommand, ResponseDTO>
    {
        private readonly ICarService _carService;

        public AddCarCommandHandler(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<ResponseDTO> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDTO();

            response.Result = await _carService.AddCar(request.carDTO);

            return response;
        }
    }
}
