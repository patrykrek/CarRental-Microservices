using app.Services.AuthAPI.Data;
using app.Services.AuthAPI.Models;
using app.Services.AuthAPI.Models.DTO;
using app.Services.AuthAPI.Service;
using app.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace app.ApiUnitTests.AuthApiTests
{
    public class AuthServiceTests
    {
        private readonly Mock<IJwtGenerator> _jwtGeneratorMock;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {       

        }

        

    }
}