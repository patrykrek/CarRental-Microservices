using app.Web.Models.DTO;


namespace app.Web.Service.IService
{
    public interface ICarService
    {
        Task<ResponseDTO> GetAllCars();
        Task<ResponseDTO> GetCarById(int id);
        Task<ResponseDTO> AddCar(AddCarDTO carDTO);
        Task<ResponseDTO> DeleteCar(int id);
        Task<ResponseDTO> UpdateCar(UpdateCarDTO carDTO);
    }
}
