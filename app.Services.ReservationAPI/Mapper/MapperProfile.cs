using app.Services.ReservationAPI.Models;
using app.Services.ReservationAPI.Models.DTO;
using AutoMapper;

namespace app.Services.ReservationAPI.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Reservation, GetReservationDTO>();
            CreateMap<Reservation, GetUserReservationDTO>();
        }
    }
}
