
using app.Web.Commands;
using app.Web.Commands.CarCommands;
using app.Web.Models.DTO;
using app.Web.Queries;
using app.Web.Queries.CarQueries;
using app.Web.Service.IService;
using app.Web.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace app.Web.Controllers
{

    public class CarController : Controller
    {
        private readonly IMediator _mediator;

        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = SD.AdminRole)]
        public async Task<IActionResult> DisplayCars()
        {
            var list = new List<GetCarDTO>();

            var response = await _mediator.Send(new DisplayAllCarsQuery());

            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<GetCarDTO>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response.Message;
            }

            return View(list);
        }

        [Authorize(Roles = SD.AdminRole)]
        [HttpGet]
        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(AddCarDTO carDTO)
        {
            if (ModelState.IsValid)
            {
                ResponseDTO? response = await _mediator.Send(new AddCarCommand(carDTO));

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Car added successfully";

                    return RedirectToAction("Index", "Home");                   
                }
                else
                {
                    TempData["error"] = response.Message;
                }



            }
            return View(carDTO);
        }

        [Authorize(Roles = SD.AdminRole)]
        public async Task<IActionResult> DeleteCar(int id)
        {
            ResponseDTO? response = await _mediator.Send(new DeleteCarCommand(id));

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Car deleted successfully";

                return RedirectToAction("DisplayCars");
            }
            else
            {
                TempData["error"] = response.Message;
            }

            return View(response);


        }

        [Authorize(Roles = SD.AdminRole)]
        [HttpGet]
        public async Task<IActionResult> EditCar(int id)
        {
            var carForUpdate = new UpdateCarDTO();

            ResponseDTO? response = await _mediator.Send(new GetCarByIdQuery(id));

            if(response != null || response.IsSuccess)
            {
                carForUpdate = JsonConvert.DeserializeObject<UpdateCarDTO>(Convert.ToString(response.Result));
            }

            return View(carForUpdate);
        }

        [Authorize(Roles = SD.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> EditCar(UpdateCarDTO carDTO)
        {
            ResponseDTO? response = await _mediator.Send(new UpdateCarCommand(carDTO));

            if (response != null || response.IsSuccess)
            {
                TempData["success"] = "Car updated successfully";

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
