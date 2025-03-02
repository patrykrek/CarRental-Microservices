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
            return Ok(await _carService.DisplayCars());
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _carService.GetCarById(id));
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
            
            return Ok(await _carService.DeleteCar(id));

        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody ]UpdateCarDTO carDTO)
        {
            return Ok(await _carService.UpdateCar(carDTO));
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("price/{id}")]
        public async Task<ActionResult> GetPrice(int id)
        {
            return Ok(await _carService.GetCarPrice(id));
        }
    }
}
