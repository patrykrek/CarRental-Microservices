using app.Services.ReservationAPI.Models;
using app.Services.ReservationAPI.Models.DTO;
using app.Services.ReservationAPI.Repository.Interface;
using app.Services.ReservationAPI.Service.Interface;
using app.Services.ReservationAPI.Utility;
using AutoMapper;
using System.Data.Common;

namespace app.Services.ReservationAPI.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private readonly ResponseDTO _response;
        private readonly ICarService _carService;
        public ReservationService(IReservationRepository reservationRepository, IMapper mapper,ICarService carService)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _response = new ResponseDTO();
            _carService = carService;
        }
        public async Task<ResponseDTO> GetAllReservations()
        {
            try
            {
                var dbReserv = await _reservationRepository.GetAllReservations();

                _response.Result = dbReserv.Select(r => _mapper.Map<GetReservationDTO>(r)).ToList();
            }
            catch (DbException dbEx)
            {
                _response.IsSuccess = false;

                _response.Message = dbEx.Message;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;

                _response.Message = ex.Message;

            }

            return _response;
        }

        public async Task<ResponseDTO> GetUserReservations(string userId)
        {
            try
            {
                var reservation = await _reservationRepository.GetUserReservations(userId);

                if (reservation.Count == 0)
                {
                    _response.IsSuccess = false;

                    _response.Message = "You don't have any reservations";
                }
                else
                {
                    _response.Result = reservation.Select(r => _mapper.Map<GetUserReservationDTO>(r)).ToList();
                }

                              
            }
            catch (DbException dbEx)
            {
                _response.IsSuccess = false;

                _response.Message = dbEx.Message;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;

                _response.Message = ex.Message;

                
            }

            return _response;


        }
        public async Task<ResponseDTO> CreateReservation(AddReservationDTO reservationDTO)
        {
            try
            {
                var carExists = await _carService.GetCarById(reservationDTO.CarId);

                if(carExists.IsSuccess && carExists.Result != null)
                {

                    var carPriceResponse = await _carService.GetCarPrice(reservationDTO.CarId);

                    if(carPriceResponse.IsSuccess && carPriceResponse.Result != null)
                    {
                        var carPrice = Convert.ToDecimal(carPriceResponse.Result);

                        if(reservationDTO.StartDate > reservationDTO.EndDate)
                        {
                            _response.IsSuccess = false;

                            _response.Message = "Start Date can't be before End Date";

                            return _response;
                        }

                        var totalDays = (reservationDTO.EndDate - reservationDTO.StartDate).Days;

                        var reservation = new Reservation
                        {
                            CarId = reservationDTO.CarId,
                            ReservationDate = DateTime.Now,
                            StartDate = reservationDTO.StartDate,
                            EndDate = reservationDTO.EndDate,
                            UserId = reservationDTO.UserId,
                            TotalPrice = totalDays * carPrice

                        };

                        await _reservationRepository.AddReservation(reservation);
                    }

                   
                }
                else
                {
                    _response.IsSuccess = false;

                    _response.Message = "Car does not exist";

                    return _response;
                }
            }
            catch (DbException dbEx)
            {
                _response.IsSuccess = false;

                _response.Message = dbEx.Message;
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
