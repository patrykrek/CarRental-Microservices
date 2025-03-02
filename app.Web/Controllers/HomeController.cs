
using app.Web.Commands;
using app.Web.Commands.CarCommands;
using app.Web.Models;
using app.Web.Models.DTO;
using app.Web.Models.ViewModel;
using app.Web.Queries;
using app.Web.Queries.CarQueries;
using app.Web.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace app.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize(Roles = SD.AdminRole + "," + SD.UserRole)]
        public async Task<IActionResult> Index()
        {
            var list = new List<GetCarDTO>();

            var response = await _mediator.Send(new DisplayAllCarsQuery());

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<GetCarDTO>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response.Message;
            }

            return View(list);
        }

        [Authorize(Roles = SD.AdminRole + "," + SD.UserRole)]
        public async Task<IActionResult> CarDetails(int id)
        {
            var car = new GetCarDTO();          

            var response = await _mediator.Send(new GetCarByIdQuery(id));

            if (response != null && response.IsSuccess)
            {
                car = JsonConvert.DeserializeObject<GetCarDTO>(Convert.ToString(response.Result));

                var viewModel = new ReservationCarViewModel { carDTO = car };

                return View(viewModel);
            }
            else
            {
                TempData["error"] = response.Message;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
