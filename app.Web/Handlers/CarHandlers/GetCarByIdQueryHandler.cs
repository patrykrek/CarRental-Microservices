using app.Web.Models.DTO;
using app.Web.Queries.CarQueries;
using app.Web.Service.IService;
using MediatR;

namespace app.Web.Handlers.CarHandlers
{
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, ResponseDTO>
    {
        private readonly ICarService _carService;

        public GetCarByIdQueryHandler(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<ResponseDTO> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDTO();

            response = await _carService.GetCarById(request.id);

            return response;
        }
    }
}
