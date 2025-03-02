using app.Services.ReservationAPI.Models.DTO;
using app.Services.ReservationAPI.Service.Interface;
using app.Services.ReservationAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace app.Services.ReservationAPI.Controllers
{
    [ApiController]
    [Route("api/reservation")]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;

        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult> DisplayAllReservations()
        {
            return Ok(await _reservationService.GetAllReservations());
        }

        [Authorize(Roles = "User")]
        [HttpGet("{userId}")]
        public async Task<ActionResult> DisplayUserReservations(string userId)
        {
            return Ok(await _reservationService.GetUserReservations(userId));
        }


        [Authorize(Roles = "User")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateReservation([FromBody] AddReservationDTO reservationDTO)
        {
            return Ok(await _reservationService.CreateReservation(reservationDTO));
        }
    }
}
