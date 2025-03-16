using app.Web.Commands.ReservationCommands;
using app.Web.Models.DTO;
using app.Web.Models.ViewModel;
using app.Web.Queries;
using app.Web.Queries.ReservationQueries;
using app.Web.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace app.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = SD.UserRole)]
        public async Task<IActionResult> CreateReservation(ReservationCarViewModel viewmodel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            viewmodel.UserId = userId;

            var response = await _mediator.Send(new CreateReservationCommand(viewmodel));

            if(response != null && response.IsSuccess)
            {
                TempData["success"] = "Reservation created successfully";

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = response.Message;

                return RedirectToAction("Index", "Home");
            }

            

        }

        [Authorize(Roles = SD.AdminRole)]
        public async Task<IActionResult> DisplayReservations()
        {
            var list = new List<GetReservationDTO>();

            var response = await _mediator.Send(new GetAllReservationsQuery());

            if(response.IsSuccess && response != null)
            {
                list = JsonConvert.DeserializeObject<List<GetReservationDTO>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response.Message;
            }

            return View(list);  
        }

        [Authorize(Roles =  SD.UserRole)]
        public async Task<IActionResult> DisplayUserReservations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var response = new ResponseDTO();

            var list = new List<GetUserReservationDTO>();

            response = await _mediator.Send(new GetUserReservationsQuery(userId));

            if(response.IsSuccess && response != null)
            {
                list = JsonConvert.DeserializeObject<List<GetUserReservationDTO>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response.Message;
            }

            return View(list);
        }
    }
}
