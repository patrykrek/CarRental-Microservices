using app.Services.ReservationAPI.Models.DTO;
using app.Services.ReservationAPI.Service.Interface;
using app.Services.ReservationAPI.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace app.Services.ReservationAPI.Service
{
    public class CarService : ICarService
    {
        private readonly IBaseService _baseService;


        public CarService(IBaseService baseService)
        {
            _baseService = baseService;

        }

        public async Task<ResponseDTO> GetCarById(int carId)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ApiGatewayBase + "/api/cars/" + carId
            });

        }

        public async Task<ResponseDTO> GetCarPrice(int carId)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ApiGatewayBase + "/api/cars/price/" + carId
            });
        }
    }
}
