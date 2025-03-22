using app.Services.AuthAPI.Models;
using app.Services.AuthAPI.Models.DTO;
using AutoMapper;

namespace app.Services.AuthAPI.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ApplicationUser, GetUserDTO>();
        }
    }
}
