using app.Web.Models.DTO;
using app.Web.Service;
using app.Web.Service.IService;
using app.Web.Utility;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.ApiUnitTests.WebTests
{
    public class ReservationServiceTests
    {
        private readonly Mock<IBaseService> _baseServiceMock;
        private readonly ReservationService _reservationService;

        public ReservationServiceTests()
        {
            _baseServiceMock = new Mock<IBaseService>();
            _reservationService = new ReservationService(_baseServiceMock.Object);
        }

        [Fact]
        public async Task CreateReservation_ShouldReturnResponse_WhenApiCallIsSuccessfull()
        {
            //arrange
            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,

            };

            var reservationDTO = new AddReservationDTO
            {
                CarId = 1,
                EndDate = new DateTime(2025, 02, 23),
                StartDate = new DateTime(2025, 02, 20)               
            };


            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _reservationService.CreateReservation(reservationDTO);

            //assert
            Assert.True(result.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task CreateReservation_ShouldReturnFalseSuccessAndMessage_WhenApiCallFailed()
        {
            //arrange
            var reservationDTO = new AddReservationDTO
            {
                CarId = 1,
                EndDate = new DateTime(2025, 02, 23),
                StartDate = new DateTime(2025, 02, 20)
            };

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = false,
                Message = "Something went wrong"

            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _reservationService.CreateReservation(reservationDTO);

            //assert

            Assert.False(result.IsSuccess);

            Assert.Equal(expectedResponse.Message, result.Message);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task CreateReservation_ShouldSendCorrectRequest()
        {
            //arrange
            var reservationDTO = new AddReservationDTO
            {
                CarId = 1,
                EndDate = new DateTime(2025, 02, 23),
                StartDate = new DateTime(2025, 02, 20)
            };

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,              

            };

            _baseServiceMock.Setup(s => s.SendAsync(It.Is<RequestDTO>(req =>
            req.ApiType == SD.ApiType.POST && req.Url == SD.ReservationApiBase + "/api/reservation/create" && req.Data == reservationDTO), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);
            //act

            var result = await _reservationService.CreateReservation(reservationDTO);

            //assert
            Assert.True(result.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task GetAllReservation_ShouldReturnResponse_WhenApiCallIsSuccessfull()
        {
            //arrange
            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _reservationService.GetAllReservations();

            //assert
            Assert.True(result.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task GetAllReservations_ShouldReturnFalseSuccessAndMessage_WhenApiCallFailed()
        {
            var expectedResponse = new ResponseDTO
            {
                IsSuccess = false,
                Message = "Something went wrong"

            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _reservationService.GetAllReservations();

            //assert

            Assert.False(result.IsSuccess);

            Assert.Equal(expectedResponse.Message, result.Message);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task GetAllReservations_ShouldSendCorrectRequest()
        {
            //arrange
            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,

            };

            _baseServiceMock.Setup(s => s.SendAsync(It.Is<RequestDTO>(req =>
            req.ApiType == SD.ApiType.GET && req.Url == SD.ReservationApiBase + "/api/reservation"), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);
            //act

            var result = await _reservationService.GetAllReservations();

            //assert
            Assert.True(result.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task GetUserReservations_ShouldReturnResponse_WhenApiCallIsSuccessfull()
        {
            //arrange

            string userId = "userId";

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _reservationService.GetUserReservations(userId);

            //assert
            Assert.True(result.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task GetUserReservations_ShouldReturnFalseSuccessAndMessage_WhenApiCallFailedl()
        {
            //arrange

            string userId = "userId";

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = false,
                Message = "Something went wrong"
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _reservationService.GetUserReservations(userId);

            //assert
            Assert.False(result.IsSuccess);

            Assert.Equal(expectedResponse.Message, result.Message);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task GetUserReservations_ShouldSendCorrectRequest()
        {
            //arrange

            string userId = "userId";   

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,

            };

            _baseServiceMock.Setup(s => s.SendAsync(It.Is<RequestDTO>(req =>
            req.ApiType == SD.ApiType.GET && req.Url == SD.ReservationApiBase + "/api/reservation/" + userId), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);
            //act

            var result = await _reservationService.GetUserReservations(userId);

            //assert
            Assert.True(result.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

    }
}
