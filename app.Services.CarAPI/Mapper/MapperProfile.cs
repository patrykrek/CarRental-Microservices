using app.Services.CarAPI.Models;
using app.Services.CarAPI.Models.DTO;
using AutoMapper;

namespace app.Services.CarAPI.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Car, GetCarDTO>();
        }
    }
}
