using app.Services.CarAPI.Data;
using app.Services.CarAPI.Models;
using app.Services.CarAPI.Repositories;
using app.Services.CarAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.ApiUnitTests.CarApiTests.RepositoryTests
{
    public class CarRepositoryTest
    {
        private readonly DataContext _context;
        private readonly CarRepository _carRepository;

        public CarRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "CarDatabase").Options;

            _context = new DataContext(options);

            _context.Cars.AddRange(new List<Car>
            {
                new Car 
                { 
                    Id = 1,
                    Make = "Toyota",
                    Model = "Camry",
                    Type = "Sedan",
                    Description = "Reliable car",
                    PricePerDay = 50, 
                    Year = 2020,
                    ImageUrl = "imageurl.com" 
                },

                new Car 
                { 
                    Id = 2,
                    Make = "Honda", 
                    Model = "Civic",
                    Type = "Sedan",
                    Description = "Compact and efficient",
                    PricePerDay = 40,
                    Year = 2019, 
                    ImageUrl = "imageurl2.com" 
                }
            });

            _context.SaveChanges();

            _carRepository = new CarRepository(_context);
        }

        [Fact]
        public async Task GetAllCars_ReturnsAllCars()
        {           
            //act

            var cars = await _carRepository.GetAllCars();

            //assert
            Assert.Equal(2, cars.Count);
        }

        [Fact]
        public async Task GetCarById_ReturnsCorrectCar()
        {
            //act

            var car = await _carRepository.GetCarById(1);

            //assert
            Assert.NotNull(car);

            Assert.Equal(1, car.Id);

            Assert.Equal("Toyota", car.Make);

        }

        [Fact]
        public async Task AddCar_ShouldAddNewCarToDatabase()
        {
            //arrange
            var newCar = new Car
            {
                Id = 3,
                Make = "Lamborghini",
                Model = "Huracan",
                Type = "Coupe",
                Description = "Reliable car",
                PricePerDay = 1000,
                Year = 2020,
                ImageUrl = "imageurl4.com"
            };

            //act

            await _carRepository.AddCar(newCar);

            var cars = await _carRepository.GetAllCars();

            //assert
            Assert.Equal(3, cars.Count);

        }

        [Fact]
        public async Task DeleteCar_ShouldDeleteCarFromDatabase()
        {
            //arrange
            var carForDelete = await _carRepository.GetCarById(1);

            //act

            await _carRepository.DeleteCar(carForDelete);

            var cars = await _carRepository.GetAllCars();

            //assert

            Assert.Single(cars);
        }

        [Fact]
        public async Task FindCar_ReturnsCorrectCar()
        {
            //act

            var car = await _carRepository.FindCar(1);

            //assert

            Assert.NotNull(car);

            Assert.Equal(1, car.Id);

            Assert.Equal("Toyota", car.Make);
        }

        [Fact]
        public async Task UpdateCar_ShouldUpdateCarInDatabase()
        {
            //arrange

            var carForUpdate = await _carRepository.GetCarById(1);

            carForUpdate.PricePerDay = 1000;

            //act

            await _carRepository.UpdateCar(carForUpdate);

            var car = await _carRepository.GetCarById(1);

            //assert
            Assert.Equal(1000, car.PricePerDay);
        }
    }
}
