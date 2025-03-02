using app.Services.CarAPI.Models;

namespace app.Services.CarAPI.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllCars();
        Task<Car> GetCarById(int id);
        Task<Car> FindCar(int id);
        Task AddCar(Car car);
        Task DeleteCar(Car car);
        Task UpdateCar(Car car);
    }
}
