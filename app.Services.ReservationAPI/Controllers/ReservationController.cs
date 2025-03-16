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
            var response = await _reservationService.GetAllReservations();

            if(response.Result == null)
            {
                return NotFound($"Reservation list is empty");
            }

            return Ok(response);
        }

        [Authorize(Roles = "User")]
        [HttpGet("{userId}")]
        public async Task<ActionResult> DisplayUserReservations(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest($"User ID must be a string");  
            }
            var response = await _reservationService.GetUserReservations(userId);

            if(response.Result == null)
            {
                return NotFound($"You don't have any reservations");
            }

            return Ok(response);
        }


        [Authorize(Roles = "User")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateReservation([FromBody] AddReservationDTO reservationDTO)
        {
            if (string.IsNullOrEmpty(reservationDTO.UserId))
            {
                return BadRequest($"User ID must be a string");
            }

            if(reservationDTO.CarId <= 0)
            {
                return BadRequest($"Car ID must be higher than 0");
            }

            var response = await _reservationService.CreateReservation(reservationDTO);

            return Ok(response);
        }
    }
}
