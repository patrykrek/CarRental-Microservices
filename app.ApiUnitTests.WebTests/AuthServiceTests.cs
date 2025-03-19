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
    public class AuthServiceTests
    {
        private readonly Mock<IBaseService> _baseServiceMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _baseServiceMock = new Mock<IBaseService>();
            _authService = new AuthService( _baseServiceMock.Object);
        }

        [Fact]
        public async Task AssignRole_ShouldReturnResponse_WhenApiCallIsSuccessfull()
        {
            //arrange
            var registrationDTO = new RegistrationRequestDTO
            {
                Email = "email@email.com",
                Password = "password",
                PhoneNumber = "123456789",
                Name = "name",
                Role = "User"
            };

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,

            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);

            //act

            var result = await _authService.AssignRole(registrationDTO);

            //assert
            Assert.True(result.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task AssignRole_ShouldReturnFalseSuccessAndMessage_WhenApiCallFailed()
        {
            //arrange
            var registrationDTO = new RegistrationRequestDTO
            {
                Email = "email@email.com",
                Password = "password",
                PhoneNumber = "123456789",
                Name = "name",
                Role = "User"
            };

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = false,
                Message = "Something went wrong"
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
               .ReturnsAsync(expectedResponse);

            //act

            var result = await _authService.AssignRole(registrationDTO);

            //assert
            Assert.False(result.IsSuccess);

            Assert.Equal(expectedResponse.Message, result.Message);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task AssignRole_ShouldSendCorrectRequest()
        {
            //arrange
            var registrationDTO = new RegistrationRequestDTO
            {
                Email = "email@email.com",
                Password = "password",
                PhoneNumber = "123456789",
                Name = "name",
                Role = "User"
            };

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,
                
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.Is<RequestDTO>(req =>
            req.ApiType == SD.ApiType.POST && req.Url == SD.AuthApiBase + "/api/auth/assignrole" && req.Data == registrationDTO), It.IsAny<bool>()))
                .ReturnsAsync(expectedResponse);


            //act

            var result = await _authService.AssignRole(registrationDTO);


            //assert

            Assert.True(result.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task Login_ShouldReturnResponse_WhenApiCallIsSuccessfull()
        {
            //arrange
            var loginDTO = new LoginRequestDTO
            {
                UserName = "name",
                Password = "password",
            };

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
               .ReturnsAsync(expectedResponse);

            //act

            var result = await _authService.Login(loginDTO);

            //assert

            Assert.True(result.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task Login_ShouldReturnFalseSuccessAndMessage_WhenApiCallFailed()
        {
            //arrange
            var loginDTO = new LoginRequestDTO
            {
                UserName = "name",
                Password = "password",
            };

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = false,
                Message = "Something went wrong"
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
               .ReturnsAsync(expectedResponse);

            //act

            var result = await _authService.Login(loginDTO);

            //assert
            Assert.False(result.IsSuccess);

            Assert.Equal(expectedResponse.Message, result.Message);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task Login_ShouldSendCorrectRequest()
        {
            //arrange
            var loginDTO = new LoginRequestDTO
            {
                UserName = "name",
                Password = "password",
            };

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true,
                
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.Is<RequestDTO>(req =>
             req.ApiType == SD.ApiType.POST && req.Url == SD.AuthApiBase + "/api/auth/login" && req.Data == loginDTO), It.IsAny<bool>()))
               .ReturnsAsync(expectedResponse);

            //act

            var result = await _authService.Login(loginDTO);

            //assert
            Assert.True(result.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }
        [Fact]
        public async Task Register_ShouldReturnResponse_WhenApiCallIsSuccessfull()
        {
            //arrange
            var registrationDTO = new RegistrationRequestDTO
            {
                Email = "email@email.com",
                Password = "password",
                PhoneNumber = "123456789",
                Name = "name",
                Role = "User"
            };

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
               .ReturnsAsync(expectedResponse);

            //act

            var result = await _authService.Register(registrationDTO);

            //assert

            Assert.True(result.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task Register_ShouldReturnFalseSuccessAndMessage_WhenApiCallFailed()
        {
            //arrange
            var registrationDTO = new RegistrationRequestDTO
            {
                Email = "email@email.com",
                Password = "password",
                PhoneNumber = "123456789",
                Name = "name",
                Role = "User"
            };

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = false,
                Message = "Something went wrong"
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()))
               .ReturnsAsync(expectedResponse);

            //act

            var result = await _authService.Register(registrationDTO);

            //assert

            Assert.False(result.IsSuccess);

            Assert.Equal(expectedResponse.Message, result.Message);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task Register_ShouldSendCorrectRequest()
        {
            //arrange
            var registrationDTO = new RegistrationRequestDTO
            {
                Email = "email@email.com",
                Password = "password",
                PhoneNumber = "123456789",
                Name = "name",
                Role = "User"
            };

            var expectedResponse = new ResponseDTO
            {
                IsSuccess = true
            };

            _baseServiceMock.Setup(s => s.SendAsync(It.Is<RequestDTO>(req =>
             req.ApiType == SD.ApiType.POST && req.Url == SD.AuthApiBase + "/api/auth/register" && req.Data == registrationDTO), It.IsAny<bool>()))
               .ReturnsAsync(expectedResponse);

            //act

            var result = await _authService.Register(registrationDTO);

            //assert

            Assert.True(result.IsSuccess);

            _baseServiceMock.Verify(s => s.SendAsync(It.IsAny<RequestDTO>(), It.IsAny<bool>()), Times.Once);
        }

    }
}
