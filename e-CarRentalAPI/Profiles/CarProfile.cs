using AutoMapper;
using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;

namespace e_CarRentalAPI.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarListDTO>()
                .ForMember(b => b.FullName, t => t.MapFrom(a => $"{a.Brand} {a.Model}"));

            CreateMap<CarDTO, Car>().ReverseMap();
        }
    }
}
