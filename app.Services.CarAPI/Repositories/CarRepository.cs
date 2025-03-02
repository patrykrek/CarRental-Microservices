using app.Services.CarAPI.Data;
using app.Services.CarAPI.Models;
using app.Services.CarAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace app.Services.CarAPI.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext _context;

        public CarRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Car>> GetAllCars()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetCarById(int id)
        {
            return await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Car> FindCar(int id)
        {
            return await _context.Cars.FindAsync(id);

        }
        public async Task AddCar(Car car)
        {
             _context.Cars.Add(car);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCar(Car car)
        {
            _context.Cars.Remove(car);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCar(Car car)
        {
            _context.Cars.Update(car);

            await _context.SaveChangesAsync();
        }

    }
}
