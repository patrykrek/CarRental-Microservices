using app.Services.AuthAPI.Models.DTO;
using app.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.Services.AuthAPI.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponseDTO _response;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _response = new ResponseDTO();
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody]RegistrationRequestDTO requestDTO)
        {
            var errorMessage = await _authService.Register(requestDTO);

            if(!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;

                _response.Message = errorMessage;

                return BadRequest(errorMessage);
            }

            return Ok(_response);



        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDTO requestDTO)
        {
            var loginResponse = await _authService.Login(requestDTO);

            if(loginResponse.User == null)
            {
                _response.IsSuccess = false;

                _response.Message = "Username or password is incorrect";

                return BadRequest(loginResponse);
            }

            _response.Result = loginResponse;

            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        public async Task<ActionResult> AssignRole([FromBody] RegistrationRequestDTO requestDTO)
        {
            var AssignRoleSucces = await _authService.AssignRole(requestDTO.Email, requestDTO.Role);

            if (!AssignRoleSucces)
            {
                _response.IsSuccess = false;

                _response.Message = "Role assignment failed";

                return BadRequest(_response);
            }

            return Ok(_response);
        }
    }
}
