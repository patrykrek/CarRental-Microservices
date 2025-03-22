using app.Services.AuthAPI.Models;
using app.Services.AuthAPI.Models.DTO;
using app.Services.AuthAPI.Repository.Interface;
using app.Services.AuthAPI.Service;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.ApiUnitTests.AuthApiTests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_mapperMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnResponse_WhenEverythingIsCorrect()
        {
            //arrange
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser{Id = "test", Email = "test@test.com", PhoneNumber = "123456789"},
                new ApplicationUser{Id = "test2", Email = "test2@test.com", PhoneNumber = "987654321"}
            };

            var usersDTO = new List<GetUserDTO>()
            {
                new GetUserDTO{ Id = "test", Email = "test@test.com", PhoneNumber = "123456789" },
                new GetUserDTO{Id = "test2", Email = "test2@test.com", PhoneNumber = "987654321"}
            };

            _userRepositoryMock.Setup(repo => repo.GetAllUsers())
                .ReturnsAsync(users);

            _mapperMock.Setup(m => m.Map<List<GetUserDTO>>(It.IsAny<ApplicationUser>))
                .Returns(usersDTO);


            //act

            var response = await _userService.GetUsers();

            //assert
            Assert.NotNull(response);

            Assert.True(response.IsSuccess);

            
        }

        [Fact]
        public async Task GetAllUser_WhenExceptionIsThrown_ShouldReturnErrorResponse()
        {
            //arrange

            _userRepositoryMock.Setup(repo => repo.GetAllUsers()).ThrowsAsync(new Exception("Something went wrong with database"));

            //act
            var response = await _userService.GetUsers();

            //assert

            Assert.Equal("Something went wrong with database", response.Message);

            Assert.False(response.IsSuccess);

        }

        [Fact]
        public async Task DeleteUser_ShouldDeleteCarById_WhenEverythingIsCorrect()
        {
            //arrange
            var userId = "userId";

            var user = new ApplicationUser
            {
                Id = "test",
                Email = "test@test.com",
                PhoneNumber = "123456789"
            };

            _userRepositoryMock.Setup(repo => repo.FindUser(userId)).ReturnsAsync(user);

            _userRepositoryMock.Setup(repo => repo.DeleteUser(It.IsAny<ApplicationUser>()))
                .Returns(Task.CompletedTask);

            //act

            var response = await _userService.DeleteUser(userId);

            //assert
            _userRepositoryMock.Verify(repo => repo.DeleteUser(It.IsAny<ApplicationUser>()), Times.Once);

            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnFalseSuccessAndMessage_WhenUserIsNotFound()
        {
            //arrange
            var userId = "userId";

            _userRepositoryMock.Setup(repo => repo.FindUser(userId)).ReturnsAsync((ApplicationUser)null);

            //act
            var response = await _userService.DeleteUser(userId);

            //assert

            Assert.NotNull(response);

            Assert.False(response.IsSuccess);

            Assert.Equal("User not found", response.Message);
            
        }

        [Fact]
        public async Task DeleteUser_WhenExceptionIsThrown_ShouldReturnFalseSuccessAndMessage()
        {
            //arrange
            var userId = "userId";

            _userRepositoryMock.Setup(repo => repo.FindUser(userId)).ThrowsAsync(new Exception("Something went wrong with database"));


            //act
            var response = await _userService.DeleteUser(userId);

            //assert

            Assert.False(response.IsSuccess);

            Assert.Equal("Something went wrong with database", response.Message);

        }

    }
}
