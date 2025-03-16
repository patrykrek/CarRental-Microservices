using app.Services.CarAPI.Models.DTO;
using app.Services.CarAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.Services.CarAPI.Controllers
{
    [ApiController]
    [Route("api/cars")]
    [Authorize]
    
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        
        public async Task<ActionResult> Get()
        {
            var response = await _carService.GetCars();

            if(response.Result == null)
            {
                return NotFound($"Car list is empty");
            }

            return Ok(response);

        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            if(id <= 0)
            {
                return BadRequest($"ID can't be less than 0");
            }

            var response = await _carService.GetCarById(id);

            if(response.Result == null)
            {
                return NotFound($"Car with ID {id} doesn't exist");
            }

            return Ok(response);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
       
        public async Task<ActionResult> Add([FromForm] AddCarDTO car)
        {
            await _carService.AddCar(car);

            return Ok(car);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"ID can't be less than 0");
            }

            var response = await _carService.DeleteCar(id);

            return Ok(response);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody ]UpdateCarDTO carDTO)
        {
            if(carDTO.Id <= 0)
            {
                return BadRequest($"ID can't be less than 0");
            }
            var response = await _carService.UpdateCar(carDTO);

            return Ok(response);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("price/{id}")]
        public async Task<ActionResult> GetPrice(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"ID can't be less than 0");
            }
            var response = await _carService.GetCarPrice(id);

            return Ok(response);
        }
    }
}
