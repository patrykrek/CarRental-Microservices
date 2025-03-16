using app.Services.CarAPI.Models.DTO;

namespace app.Services.CarAPI.Services.Interfaces
{
    public interface ICarService
    {
        Task<ResponseDTO> GetCars();
        Task<ResponseDTO> GetCarById(int id);
        Task<ResponseDTO> AddCar(AddCarDTO car);
        Task<ResponseDTO> DeleteCar(int id);
        Task<ResponseDTO> UpdateCar(UpdateCarDTO carDTO);
        Task<ResponseDTO> GetCarPrice(int id);
    }
}
