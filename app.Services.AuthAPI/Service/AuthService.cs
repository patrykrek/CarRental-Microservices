using app.Services.AuthAPI.Data;
using app.Services.AuthAPI.Models;
using app.Services.AuthAPI.Models.DTO;
using app.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace app.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;

        public AuthService(IJwtGenerator jwtGenerator, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, DataContext context)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

            if(user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, roleName);

                return true;
            }

            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var user = _context.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());

                var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

                if (isValid == false || user == null)
                {
                    return new LoginResponseDTO 
                    {
                        User = null,
                        Token = "",
                        Response = new ResponseDTO
                        {
                            IsSuccess = false,
                            Message = "Incorrect login or password"
                        }
                    };
                }

                var roles = await _userManager.GetRolesAsync(user);

                var token = _jwtGenerator.GenerateToken(user, roles);

                var userDTO = new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Name = user.UserName,


                };

                LoginResponseDTO loginResponseDTO = new LoginResponseDTO
                {
                    User = userDTO,
                    Token = token,

                };

                return loginResponseDTO;

            }
            catch (Exception ex)
            {

                return new LoginResponseDTO
                {
                    User = null,
                    Token = "",
                    Response = new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "An unexpected error occurred. Please try again later."
                    }
                };


            }

            

        }

        public async Task<string> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            var user = new ApplicationUser
            {
                Email = registrationRequestDTO.Email,
                UserName = registrationRequestDTO.Email,
                PhoneNumber = registrationRequestDTO.PhoneNumber,
                Name = registrationRequestDTO.Name,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),

            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);

                if (result.Succeeded)
                {
                    var userToReturn = _context.ApplicationUsers.FirstOrDefault(u => u.UserName == user.Email);

                    UserDTO userDTO = new UserDTO
                    {
                        Id = userToReturn.Id,
                        Email = userToReturn.Email,
                        PhoneNumber = userToReturn.PhoneNumber,
                        Name = userToReturn.UserName,
                    };

                    return "";

                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }

                
            }
            catch (Exception)
            {

                
            }
            return "Error";
        }


    }
}
