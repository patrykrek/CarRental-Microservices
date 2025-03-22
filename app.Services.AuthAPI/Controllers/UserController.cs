using app.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.Services.AuthAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
     
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {

            var response = await _userService.GetUsers();

            if (response.Result == null)
            {
                return NotFound($"Users not found");
            }

            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("{userId}")]
        public async Task<ActionResult> Delete(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest($"User ID can't be empty");
            }

            var response = await _userService.DeleteUser(userId);

            return Ok(response);
        }
    }
}
