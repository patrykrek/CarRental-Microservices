using app.Web.Commands.UserCommands;
using app.Web.Models.DTO;
using app.Web.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace app.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> DisplayUsers()
        {
            var list = new List<GetUserDTO>();

            var response = await _mediator.Send(new GetAllUsersQuery());

            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<GetUserDTO>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response.Message;
            }

            return View(list);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var response = await _mediator.Send(new DeleteUserCommand(id));

            if(response != null && response.IsSuccess)
            {
                TempData["success"] = "User deleted successfully";

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = response.Message;
            }

            return View(response);
        }
    }
}
