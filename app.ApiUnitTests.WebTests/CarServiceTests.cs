using app.Web.Models.DTO;
using app.Web.Service;
using app.Web.Service.IService;
using app.Web.Utility;
using Microsoft.AspNetCore.Http;
using Moq;

namespace app.ApiUnitTests.WebTests
{
    public class CarServiceTests
    {
        private readonly Mock<IBaseService> _baseServiceMock;
        private readonly CarService _carService;

        public CarServiceTests()
        {
            _baseServiceMock = new Mock<IBaseService>();
            _carService = new CarService(_baseServiceMock.Object);
        }
        [Fact]
        public async Task GetAllCars_ShouldReturnResponse_WhenApiCallIsSuccessfull()
        {
            //arrange
            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,
                Result = new object(),
            };

            _baseServiceMock.Setup(serv => serv.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _carService.GetAllCars();

            //assert
            Assert.Equal(expectedResponse, result);

            Assert.True(result?.IsSuccess);

        }

        [Fact]
        public async Task GetAllCars_ShouldReturnFalseSuccessAndMessage_WhenApiCallFailed()
        {
            //arrange
            var expectedResponse = new ResponseDTO
            {
                IsSuccess = false,
                Message = "Car list is empty"
            };

            _baseServiceMock.Setup(serv => serv.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
               .ReturnsAsync(expectedResponse);

            //act

            var result = await _carService.GetAllCars();

            //assert
            Assert.Equal(expectedResponse, result);

            Assert.False(result?.IsSuccess);

            Assert.Equal("Car list is empty", result?.Message);
        }

        [Fact]
        public async Task GetAllCars_ShouldSendCorrectRequest()
        {
            // Arrange
            _baseServiceMock.Setup(serv => serv.SendAsync(It.Is<RequestDTO>(
                req => req.ApiType == SD.ApiType.GET &&
                       req.Url == SD.CarApiBase + "/api/cars"
            ), It.IsAny<bool>()))
            .ReturnsAsync(new ResponseDTO { IsSuccess = true });

            // Act
            var result = await _carService.GetAllCars();

            // Assert
            Assert.True(result?.IsSuccess);

            _baseServiceMock.Verify(serv => serv.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task GetCarById_ShouldReturnResponse_WhenApiCallIsSuccessfull()
        {
            //arrange
            var carId = 1;

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,
                Result = new object()
            };

            _baseServiceMock.Setup(serv => serv.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _carService.GetCarById(carId);

            //assert

            Assert.Equal(expectedResponse, result);

            Assert.True(result?.IsSuccess);
        }

        [Fact]
        public async Task GetCarById_ShouldReturnFalseSuccessAndMessage_WhenApiCallFailed()
        {
            //arrange
            var carId = 1;

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = false,
                Message = "Car not found"
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);
            //act
            var result = await _carService.GetCarById(carId);

            //assert

            Assert.False(result?.IsSuccess);

            Assert.Equal("Car not found", result?.Message);

        }

        [Fact]
        public async Task GetCarById_ShouldSendCorrectRequest()
        {
            //arrange

            var carId = 1;

            var expectedResponse = new ResponseDTO()
            {
                IsSuccess = true,
                Result = new object()
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.Is<RequestDTO>(req =>
            req.ApiType == SD.ApiType.GET && req.Url == SD.CarApiBase + "/api/cars/" + carId), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _carService.GetCarById(carId);

            //assert
            Assert.True(result?.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task AddCar_ShouldReturnResponse_WhenApiCallIsSuccessfull()
        {
            //arrange
            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,
                Result = new object()
            };

            var carDTO = new AddCarDTO
            {
                Make = "test2",

                Model = "test2",

                Type = "test2",

                Year = 2000,

                PricePerDay = 200,

                Description = "test",

                Image = new FormFile(new MemoryStream(), 0, 0, "uploads", "image.png")
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _carService.AddCar(carDTO);

            //assert
            Assert.True(result?.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task AddCar_ShouldReturnFalseSuccessAndMessage_WhenApiCallFailed()
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

                Image = new FormFile(new MemoryStream(), 0, 0, "uploads", "image.png")
            };

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = false,
                Message = "Something went wrong"
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>())).ReturnsAsync(expectedResponse);

            //act

            var result = await _carService.AddCar(carDTO);


            //assert
            Assert.False(result?.IsSuccess);
            Assert.Equal(expectedResponse.Message, result?.Message);
        }

        [Fact]
        public async Task AddCar_ShouldSendCorrectRequest()
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

                Image = new FormFile(new MemoryStream(), 0, 0, "uploads", "image.png")
            };

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.Is<RequestDTO>(req =>
             req.ApiType == SD.ApiType.POST && req.Data == carDTO && req.Url == SD.CarApiBase + "/api/cars/add"), It.IsAny<bool>()))
                 .ReturnsAsync(expectedResponse);

            //act

            var result = await _carService.AddCar(carDTO);

            //assert
            Assert.True(result?.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task DeleteCar_ShouldReturnResponse_WhenApiCallIsSuccessfull()
        {
            //arrange
            var carId = 1;

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,
                
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _carService.DeleteCar(carId);

            //assert
            Assert.True(result?.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task DeleteCar_ShouldReturnFalseSuccessAndMessage_WhenApiCallFailed()
        {
            //arrange
            var carId = 1;

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = false,
                Message = "Something went wrong"

            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _carService.DeleteCar(carId);

            //assert
            Assert.False(result?.IsSuccess);

            Assert.Equal(expectedResponse.Message, result?.Message);
        }

        [Fact]
        public async Task DeleteCar_ShouldSendCorrectRequest()
        {
            //arrange
            var carId = 1;

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.Is<RequestDTO>(req =>
             req.ApiType == SD.ApiType.DELETE && req.Url == SD.CarApiBase + "/api/cars/" + carId), It.IsAny<bool>()))
                 .ReturnsAsync(expectedResponse);

            //act

            var result = await _carService.DeleteCar(carId);

            //assert

            Assert.True(result?.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCar_ShouldReturnResponse_WhenApiCallIsSuccessfull()
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

                Image = new FormFile(new MemoryStream(), 0, 0, "uploads", "image.png")
            };

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _carService.UpdateCar(carDTO);

            //assert
            Assert.True(result?.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCar_ShouldReturnFalseSuccessAndMessage_WhenApiCallFailed()
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

                Image = new FormFile(new MemoryStream(), 0, 0, "uploads", "image.png")
            };

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = false,
                Message = "Something went wrong"
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _carService.UpdateCar(carDTO);

            //assert
            Assert.False(result?.IsSuccess);

            Assert.Equal(expectedResponse.Message, result?.Message);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCar_ShouldSendCorrectRequest()
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

                Image = new FormFile(new MemoryStream(), 0, 0, "uploads", "image.png")
            };

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.Is<RequestDTO>(req =>
             req.ApiType == SD.ApiType.PUT && req.Url == SD.CarApiBase + "/api/cars" && req.Data == carDTO), It.IsAny<bool>()))
                 .ReturnsAsync(expectedResponse);

            //act

            var result = await _carService.UpdateCar(carDTO);

            //assert

            Assert.True(result?.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }
    }
    
}