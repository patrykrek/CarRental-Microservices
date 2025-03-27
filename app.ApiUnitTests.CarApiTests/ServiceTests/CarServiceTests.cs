using app.Services.CarAPI.Models;
using app.Services.CarAPI.Models.DTO;
using app.Services.CarAPI.Repositories.Interfaces;
using app.Services.CarAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;

namespace app.ApiUnitTests.CarApiTests.ServiceTests
{
    public class CarServiceTests
    {

        private readonly CarService _carService;
        private readonly Mock<ICarRepository> _carRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IWebHostEnvironment> _webHostEnvironmentMock;

        public CarServiceTests()
        {
            _carRepositoryMock = new Mock<ICarRepository>();
            _mapperMock = new Mock<IMapper>();
            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            _carService = new CarService(_carRepositoryMock.Object, new ResponseDTO(), _mapperMock.Object, _webHostEnvironmentMock.Object);
        }

        [Fact]
        public async Task DisplayCars_WhenEverythingIsCorrect_ShouldReturnResponseDTO()
        {
            //arrange
            var cars = new List<Car>()
            {
                new Car { Id = 1, Make = "test", Model = "test", Type = "test", Year = 2025, PricePerDay = 100, Description = "test", ImageUrl = "test" },

                new Car { Id = 2, Make = "test2", Model = "test2", Type = "test2", Year = 2025, PricePerDay = 100, Description = "test2", ImageUrl = "test2" }
            };

            var carDTOs = new List<GetCarDTO>()
            {
                new GetCarDTO { Id = 1, Make = "test", Model = "test", Type = "test", Year = 2025, PricePerDay = 100, Description = "test", ImageUrl = "test" },

                new GetCarDTO { Id = 2, Make = "test2", Model = "test2", Type = "test2", Year = 2025, PricePerDay = 100, Description = "test2", ImageUrl = "test2"  }
            };

            _carRepositoryMock.Setup(repo => repo.GetAllCars()).ReturnsAsync(cars);

            _mapperMock.Setup(m => m.Map<List<GetCarDTO>>(It.IsAny<Car>())).Returns(carDTOs);

            //act

            var result = await _carService.GetCars();

            //assert

            Assert.NotNull(result);

            Assert.True(result.IsSuccess);

            Assert.Equal(carDTOs.Count, ((List<GetCarDTO>)result.Result).Count);
        }

        [Fact]
        public async Task DisplayCars_WhenExceptionIsThrown_ShouldReturnErrorResponse()
        {
            //arrange
            _carRepositoryMock.Setup(repo => repo.GetAllCars()).ThrowsAsync(new SystemException("Something went wrong with database"));

            //act

            var response = await _carService.GetCars();

            //assert

            Assert.NotNull(response);

            Assert.False(response.IsSuccess);

            Assert.Equal("Something went wrong with database", response.Message);
        }

        [Fact]
        public async Task GetCarById_WhenEverythingIsCorrect_ShouldReturnCarById()
        {
            //arrange
            var carId = 1;

            var car = new Car { Id = carId, Make = "test", Model = "test", Type = "test", Year = 2025, PricePerDay = 100, Description = "test", ImageUrl = "test" };

            var carDTO = new GetCarDTO { Id = carId, Make = "test", Model = "test", Type = "test", Year = 2025, PricePerDay = 100, Description = "test", ImageUrl = "test" };

            _carRepositoryMock.Setup(repo => repo.GetCarById(carId)).ReturnsAsync(car);

            _mapperMock.Setup(m => m.Map<GetCarDTO>(It.IsAny<Car>())).Returns(carDTO);

            //act

            var result = await _carService.GetCarById(carId);

            //assert

            Assert.NotNull(result);

            Assert.True(result.IsSuccess);

            Assert.Equal(carDTO, result.Result);


        }

        [Fact]
        public async Task GetCarById_ShouldReturnFalse_WhenCarIdIsIncorrect()
        {
            //arrange
            var carId = 2;

            var car = new Car { Id = 1, Make = "test", Model = "test", Type = "test", Year = 2025, PricePerDay = 100, Description = "test", ImageUrl = "test" };

            _carRepositoryMock.Setup(repo => repo.GetCarById(carId)).ReturnsAsync((Car)null);

            //act
            var result = await _carService.GetCarById(carId);

            //assert

            Assert.NotNull(result);

            Assert.False(result.IsSuccess);

            Assert.Null(result.Result);
        }

        [Fact]
        public async Task GetCarById_ShouldReturnErrorResponse_WhenExceptionIsThrown()
        {
            //arrange
            var carId = 2;

            _carRepositoryMock.Setup(repo => repo.GetCarById(carId)).ThrowsAsync(new SystemException("Problem with database"));

            //act

            var response = await _carService.GetCarById(carId);

            //assert
            Assert.NotNull(response);

            Assert.False(response.IsSuccess);

            Assert.Equal("Problem with database", response.Message);
        }

        [Fact]
        public async Task GetCarPrice_ShouldReturnCarPrice_WhenEverythingIsCorrect()
        {
            //arrange
            var carId = 1;

            var car = new Car { Id = 1, Make = "test", Model = "test", Type = "test", Year = 2025, PricePerDay = 100, Description = "test", ImageUrl = "test" };

            _carRepositoryMock.Setup(repo => repo.FindCar(carId)).ReturnsAsync(car);

            //act

            var result = await _carService.GetCarPrice(carId);

            //assert
            Assert.NotNull(result);

            Assert.True(result.IsSuccess);

            Assert.Equal(car.PricePerDay, result.Result);
        }

        [Fact]
        public async Task GetCarPrice_ShouldReturnMessageAndFalseSuccess_WhenCarIsNotFound()
        {
            //arrange
            var carId = 1;

            _carRepositoryMock.Setup(repo => repo.FindCar(carId)).ReturnsAsync((Car)null);


            //act
            var result = await _carService.GetCarPrice(carId);

            //assert
            Assert.NotNull(result);

            Assert.False(result.IsSuccess);

            Assert.Equal("Car not found", result.Message);
        }

        [Fact]
        public async Task GetCarPrice_ShouldReturnMessageAndFalseSuccess_WhenExceptionIsThrown()
        {
            //arrange
            var carId = 1;

            _carRepositoryMock.Setup(repo => repo.FindCar(carId)).ThrowsAsync(new SystemException("Problem with database"));

            //act
            var result = await _carService.GetCarPrice(carId);

            //assert

            Assert.NotNull(result);

            Assert.False(result.IsSuccess);

            Assert.Equal("Problem with database", result.Message);
        }

        [Fact]
        public async Task DeleteCar_ShouldDeleteCarById_WhenEverythingIsCorrect()
        {
            //arrange
            var carId = 1;

            var car = new Car { Id = 1, Make = "test", Model = "test", Type = "test", Year = 2025, PricePerDay = 100, Description = "test", ImageUrl = "test" };

            _carRepositoryMock.Setup(repo => repo.FindCar(carId)).ReturnsAsync(car);

            _carRepositoryMock.Setup(repo => repo.DeleteCar(car)).Returns(Task.CompletedTask);

            //act

            var result = await _carService.DeleteCar(carId);

            //assert
            Assert.NotNull(result);

            Assert.True(result.IsSuccess);

            _carRepositoryMock.Verify(repo => repo.DeleteCar(car), Times.Once);


        }

        [Fact]
        public async Task DeleteCar_ShouldReturnFalseSuccessAndMessage_WhenCarIsNotFound()
        {
            //arrange
            var carId = 1;

            _carRepositoryMock.Setup(repo => repo.FindCar(carId)).ReturnsAsync((Car)null);


            //act

            var result = await _carService.DeleteCar(carId);

            //assert
            Assert.NotNull(result);

            Assert.False(result.IsSuccess);

            Assert.Equal("Car not found", result.Message);

        }

        [Fact]
        public async Task DeleteCar_ShouldReturnFalseSuccessAndMessage_WhenExceptionIsThrown()
        {
            //arrange
            var carId = 1;

            _carRepositoryMock.Setup(repo => repo.FindCar(carId)).ThrowsAsync(new SystemException("Database problem"));


            //act
            var result = await _carService.DeleteCar(carId);

            //assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal("Database problem", result.Message);
        }

        [Fact]
        public async Task UpdateCar_ShouldUpdateCar_WhenEverythingIsCorrect()
        {
            // Arrange
            var carDTO = new UpdateCarDTO
            {
                Id = 1,
                Make = "Ford",
                Model = "Mustang",
                Type = "Coupe",
                Description = "A fast car",
                PricePerDay = 100,
                Year = 2020,
                ImageFile = new FormFile(new MemoryStream(), 0, 0, "ImageFile", "test.png")
            };

            var car = new Car
            {
                Id = 1,
                Make = "BMW",
                Model = "M3",
                Type = "Sedan",
                Description = "A luxury car",
                PricePerDay = 150,
                Year = 2019,
                ImageUrl = "https://localhost:7001/uploads/old.png"
            };

            _carRepositoryMock.Setup(repo => repo.FindCar(carDTO.Id)).ReturnsAsync(car);

            string fakePath = Path.Combine(Path.GetTempPath(), "wwwroot");

            string uploadsPath = Path.Combine(fakePath, "uploads");

            Directory.CreateDirectory(uploadsPath);

            _webHostEnvironmentMock.Setup(env => env.WebRootPath).Returns(fakePath);

            // Act
            var response = await _carService.UpdateCar(carDTO);

            // Assert
            Assert.True(response.IsSuccess);
            _carRepositoryMock.Verify(repo => repo.UpdateCar(It.IsAny<Car>()), Times.Once);

        }

        [Fact]
        public async Task UpdateCar_ShouldUpdateCarWithoutNewImage()
        {
            // Arrange
            var carDTO = new UpdateCarDTO
            {
                Id = 1,
                Make = "Ford",
                Model = "Mustang",
                Type = "Coupe",
                Description = "A fast car",
                PricePerDay = 100,
                Year = 2020
            };

            var car = new Car
            {
                Id = 1,
                Make = "BMW",
                Model = "M3",
                Type = "Sedan",
                Description = "A luxury car",
                PricePerDay = 150,
                Year = 2019,
                ImageUrl = "https://localhost:7001/uploads/old.png"
            };

            _carRepositoryMock.Setup(repo => repo.FindCar(carDTO.Id)).ReturnsAsync(car);

            // Act
            var response = await _carService.UpdateCar(carDTO);

            // Assert
            Assert.True(response.IsSuccess);
            _carRepositoryMock.Verify(repo => repo.UpdateCar(It.IsAny<Car>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCar_ShouldReturnFalseSuccessAndMessage_WhenCarIsNotFound()
        {
            //arrange
            var carDTO = new UpdateCarDTO
            {
                Id = 1,
                Make = "test2",
                Model = "test2",
                Type = "test2",
                Year = 2000,
                PricePerDay = 200,
                Description = "test",
                ImageFile = new FormFile(new MemoryStream(), 0, 0, "uploads", "image.png")
            };
           

            _carRepositoryMock.Setup(repo => repo.FindCar(carDTO.Id)).ReturnsAsync((Car)null);

            

            //act

            var response = await _carService.UpdateCar(carDTO);

            //assert

            Assert.False(response.IsSuccess);

            Assert.Equal("Car not found", response.Message);

            _carRepositoryMock.Verify(repo => repo.UpdateCar(It.IsAny<Car>()), Times.Never);


        }

        [Fact]
        public async Task UpdateCar_ShouldHandleIOException_WhenExceptionIsThrown()
        {
            //arrange
            var carDTO = new UpdateCarDTO
            {
                Id = 1,
                Make = "test2",
                Model = "test2",
                Type = "test2",
                Year = 2000,
                PricePerDay = 200,
                Description = "test",
                ImageFile = new FormFile(new MemoryStream(), 0, 0, "uploads", "image.png")
            };

            var car = new Car
            {
                Id = 1,
                Make = "BMW",
                Model = "M3",
                Type = "Sedan",
                Description = "A luxury car",
                PricePerDay = 150,
                Year = 2019,
                ImageUrl = "https://localhost:7001/uploads/old.png"
            };

            _carRepositoryMock.Setup(repo => repo.FindCar(carDTO.Id)).ReturnsAsync(car);

            string fakePath = Path.Combine(Path.GetTempPath(), "wwwroot");

            string uploadsPath = Path.Combine(fakePath, "uploads");

            Directory.CreateDirectory(uploadsPath);

            _webHostEnvironmentMock.Setup(w => w.WebRootPath).Returns(fakePath);

            _carRepositoryMock.Setup(repo => repo.UpdateCar(It.IsAny<Car>())).ThrowsAsync(new IOException("File error"));

            //act

            var response = await _carService.UpdateCar(carDTO);

            //assert
            Assert.False(response.IsSuccess);

            Assert.Equal("File error", response.Message);
        }

        [Fact]
        public async Task AddCar_ShouldAddCar_WhenEverythingIsCorrect()
        {
            //arrange
            var carDTO = new AddCarDTO
            {
                Make = "test2",

                Model = "test2",

                Type = "test2",

                Year = 2000,

                PricePerDay = 200,

                Description = "test",

                ImageFile = new FormFile(new MemoryStream(), 0, 0, "uploads", "image.png")
            };

            var car = new Car
            {
                Make = carDTO.Make,

                Model = carDTO.Model,

                Type = carDTO.Type,

                Year = carDTO.Year,

                PricePerDay = carDTO.PricePerDay,

                Description = carDTO.Description,

                ImageUrl = "https://localhost:7001/uploads/image.png"
            };

            string fakePath = Path.Combine(Path.GetTempPath(), "wwwroot");

            string uploadsPath = Path.Combine(fakePath, "uploads");

            Directory.CreateDirectory(uploadsPath);

            _webHostEnvironmentMock.Setup(webhost => webhost.WebRootPath).Returns(fakePath);

            _carRepositoryMock.Setup(repo => repo.AddCar(It.IsAny<Car>())).Returns(Task.CompletedTask);

            //act

            var result = await _carService.AddCar(carDTO);


            //assert
            Assert.NotNull(result);

            Assert.True(result.IsSuccess);

            _carRepositoryMock.Verify(repo => repo.AddCar(It.IsAny<Car>()), Times.Once);
        }

        [Fact]
        public async Task AddCar_ShouldHandleIOException_WhenExceptionIsThrown()
        {
            //arrange
            var carDTO = new AddCarDTO
            {
                Make = "test2",

                Model = "test2",

                Type = "test2",

                Year = 2000,

                PricePerDay = 200,

                Description = "test",

                ImageFile = new FormFile(new MemoryStream(), 0, 0, "uploads", "image.png")
            };

            string fakePath = Path.Combine(Path.GetTempPath(), "wwwroot");

            string uploadsPath = Path.Combine(fakePath, "uploads");

            Directory.CreateDirectory(uploadsPath);

            _webHostEnvironmentMock.Setup(w => w.WebRootPath).Returns(fakePath);

            _carRepositoryMock.Setup(repo => repo.AddCar(It.IsAny<Car>())).ThrowsAsync(new IOException("File error"));


            //act

            var response = await _carService.AddCar(carDTO);

            //assert
            Assert.False(response.IsSuccess);

            Assert.Equal("File error", response.Message);

        }

    }

    
}