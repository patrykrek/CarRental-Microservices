using app.Services.ReservationAPI.Models.DTO;
using app.Services.ReservationAPI.Service;
using app.Services.ReservationAPI.Service.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.ApiUnitTests.ReservationApiTests
{
    public class CarServiceTests
    {
        private readonly Mock<IBaseService> _baseServiceMock;
        private readonly CarService _carServiceMock;

        public CarServiceTests()
        {
            _baseServiceMock = new Mock<IBaseService>();
            _carServiceMock = new CarService(_baseServiceMock.Object); 
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

            var result = await _carServiceMock.GetCarById(carId);

            //assert

            Assert.NotNull(result.Result);

            Assert.True(result.IsSuccess);

            Assert.Equal(expectedResponse, result);

        }

        [Fact]
        public async Task GetCarById_ShouldReturnFalseSuccessAndMessage_WhenApiCallFailed()
        {
            //arrange
            var carId = 1;

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = false,
                Message = "Car does not exist"
            };

            _baseServiceMock.Setup(serv => serv.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _carServiceMock.GetCarById(carId);

            //assert
            Assert.Equal(expectedResponse, result);

            Assert.False(result.IsSuccess);

            Assert.Equal("Car does not exist", result.Message);
        }

         
        [Fact]
        public async Task GetCarPrice_ShouldReturnResponse_WhenApiCallIsSuccessfull()
        {
            //arrange

            var carId = 1;

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,
                Result = 100
            };

            _baseServiceMock.Setup(serv => serv.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _carServiceMock.GetCarPrice(carId);

            //assert

            Assert.Equal(expectedResponse, result);

            Assert.True(result.IsSuccess);

            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task GetCarPrice_ShouldReturnFalseSuccessAndMessage_WhenApiCallFailed()
        {
            //arrange
            var carId = 1;

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = false,
                Message = "Car not found"
            };

            _baseServiceMock.Setup(serv => serv.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _carServiceMock.GetCarPrice(carId);

            //assert
            Assert.False(result.IsSuccess);

            Assert.Equal("Car not found", result.Message);

            Assert.Equal(expectedResponse, result);
            
        }
    }
}
