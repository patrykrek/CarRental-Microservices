using app.Web.Models.DTO;
using app.Web.Service.IService;
using app.Web.Utility;
using Microsoft.AspNetCore.Mvc;

namespace app.Web.Service
{
    public class CarService : ICarService
    {
        private readonly IBaseService _baseService;

        public CarService(IBaseService baseService)
        {
            _baseService = baseService;
        }


        public async Task<ResponseDTO?> GetAllCars()
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.GET,

                Url = SD.ApiGatewayBase + "/api/cars"

            });
                
        }

        public async Task<ResponseDTO?> GetCarById(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.GET,

                Url = SD.ApiGatewayBase + "/api/cars/" + id

            });
        }
        public async Task<ResponseDTO?> AddCar(AddCarDTO carDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.POST,

                Data = carDTO,

                Url = SD.ApiGatewayBase + "/api/cars/add"
                                
            });
        }

        public async Task<ResponseDTO?> DeleteCar(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.DELETE,

                Url = SD.ApiGatewayBase + "/api/cars/" + id

            });
        }       
        public async Task<ResponseDTO?> UpdateCar(UpdateCarDTO carDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.PUT,

                Data = carDTO,

                Url = SD.ApiGatewayBase + "/api/cars"
            });
        }
    }
}
