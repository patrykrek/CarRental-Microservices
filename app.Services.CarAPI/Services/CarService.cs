using app.Services.CarAPI.Models;
using app.Services.CarAPI.Models.DTO;
using app.Services.CarAPI.Repositories.Interfaces;
using app.Services.CarAPI.Services.Interfaces;
using AutoMapper;
using static System.Net.Mime.MediaTypeNames;

namespace app.Services.CarAPI.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ResponseDTO _response;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CarService(ICarRepository carRepository, ResponseDTO response, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _carRepository = carRepository;

            _response = new ResponseDTO();

            _mapper = mapper;

            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<ResponseDTO> DisplayCars()
        {
            try
            {
                var carsDb = await _carRepository.GetAllCars();

                _response.Result = carsDb.Select(c => _mapper.Map<GetCarDTO>(c)).ToList();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;

                _response.Message = ex.Message;

               
            }

            return _response;
        }
        public async Task<ResponseDTO> GetCarById(int id)
        {
            try
            {
                var car = await _carRepository.GetCarById(id);

                if(car == null)
                {
                    _response.IsSuccess = false;
                }

                _response.Result = _mapper.Map<GetCarDTO>(car);
              
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;

                _response.Message = ex.Message;

                
            }

            return _response;
           
        }      
        public async Task<ResponseDTO> AddCar(AddCarDTO car)
        {
            try
            {           
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(car.ImageFile.FileName)}";

                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await car.ImageFile.CopyToAsync(stream);
                }

                var newCar = new Car
                {
                    Make = car.Make,
                    Model = car.Model,
                    Description = car.Description,
                    Type = car.Type,
                    ImageUrl = $"https://localhost:7001/uploads/{fileName}",
                    PricePerDay = car.PricePerDay,
                    Year = car.Year,
                };

                await _carRepository.AddCar(newCar);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;

                _response.Message = ex.Message;

               
            }

            return _response;
            
        }

        public async Task<ResponseDTO> DeleteCar(int id)
        {
            try
            {
                var findCar = await _carRepository.FindCar(id);

                if (findCar == null)
                {
                    _response.IsSuccess = false;

                    _response.Message = "Car not found";
                }

                await _carRepository.DeleteCar(findCar);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;

                _response.Message = ex.Message;
                
            }

            return _response;
        }

        public async Task<ResponseDTO> UpdateCar(UpdateCarDTO car)
        {
            try
            {
                var getCar = await _carRepository.FindCar(car.Id);
                
                if(getCar == null)
                {
                    _response.IsSuccess = false;

                    _response.Message = "Car not found";
                }
                else
                {
                    getCar.Make = car.Make;

                    getCar.Model = car.Model;

                    getCar.Type = car.Type;

                    getCar.Description = car.Description;

                    getCar.PricePerDay = car.PricePerDay;

                    getCar.Year = car.Year;

                    await _carRepository.UpdateCar(getCar);
                }               

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;

                _response.Message = ex.Message;

                
            }

            return _response;
        }

        public async Task<ResponseDTO> GetCarPrice(int id)
        {
            try
            {
                var getCar = await _carRepository.FindCar(id);

                if (getCar == null)
                {
                    _response.IsSuccess = false;

                    _response.Message = "Car not found";
                }
                else
                {

                    _response.Result = getCar.PricePerDay;
                }
            }

            catch (Exception ex)
            {

                _response.IsSuccess = false;

                _response.Message = ex.Message;
            }

            return _response;
        }
    }
}
