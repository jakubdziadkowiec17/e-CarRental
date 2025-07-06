using AutoMapper;
using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;

namespace e_CarRentalAPI.Profiles
{
    public class RentalProfile : Profile
    {
        public RentalProfile()
        {
            //CreateMap<Rental, RentalDTO>()
            //    .ForMember(b => b.FullName, t => t.MapFrom(a => $"{a.Brand} {a.Model}"));

            CreateMap<RentalDTO, Car>().ReverseMap();
        }
    }
}
