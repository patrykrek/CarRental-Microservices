using app.Web.Commands.CarCommands;
using app.Web.Models.DTO;
using app.Web.Queries.CarQueries;
using app.Web.Service.IService;
using MediatR;

namespace app.Web.Handlers.CarHandlers
{
    public class DisplayCarsQueryHandler : IRequestHandler<DisplayAllCarsQuery, ResponseDTO>
    {
        private readonly ICarService _carService;

        public DisplayCarsQueryHandler(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<ResponseDTO> Handle(DisplayAllCarsQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDTO();

            response = await _carService.GetAllCars();

            return response;
        }
    }
}
