using app.Web.Commands;
using app.Web.Commands.AuthCommands;
using app.Web.Models.DTO;
using app.Web.Service.IService;
using app.Web.Utility;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace app.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;

        private readonly ITokenProvider _tokenProvider;

        public AuthController(IMediator mediator, ITokenProvider tokenProvider)
        {
            _mediator = mediator;

            _tokenProvider = tokenProvider;
        }
        public IActionResult Register()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem { Value = "Admin", Text = SD.AdminRole },
                new SelectListItem { Value = "User", Text = SD.UserRole }
            };

            ViewBag.Roles = list;

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDTO requestDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new RegisterCommand(requestDTO));

                if (result != null && result.IsSuccess)
                {
                    if (string.IsNullOrEmpty(requestDTO.Role)) // jezeli rola nie zostala wybrana to defaultowo ustawi sie rola user
                    {
                        requestDTO.Role = SD.UserRole;
                    }

                    var assignRole = await _mediator.Send(new AssignRoleCommand(requestDTO));

                    if (assignRole.IsSuccess)
                    {
                        return RedirectToAction("Login");
                    }


                }
            }
           
            var list = new List<SelectListItem>
            {
                new SelectListItem { Value = "Admin", Text = SD.AdminRole },
                new SelectListItem { Value = "User", Text = SD.UserRole }
            };

            ViewBag.Roles = list;

            return View(requestDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO requestDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new LoginCommand(requestDTO));

                if (response != null && response.IsSuccess)
                {                    
                    LoginResponseDTO loginResponseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));

                    if(loginResponseDTO != null)
                    {
                        await SignInUser(loginResponseDTO);

                        _tokenProvider.SetToken(loginResponseDTO.Token);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["error"] = "Incorrect login or password";
                    }

                    

                }
                
            }
           

            return View(requestDTO);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            _tokenProvider.ClearToken();

            return RedirectToAction("Index", "Home");
        }
        private async Task SignInUser(LoginResponseDTO loginResponseDTO)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(loginResponseDTO.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            var userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;

            if (userId != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
            }

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        }
    }
}
