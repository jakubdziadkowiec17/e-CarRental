using AutoMapper;
using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;

namespace e_CarRentalAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            CreateMap<ApplicationUser, EmployeeListDTO>()
                .ForMember(b => b.FullName, t => t.MapFrom(a => GetFullName(a)))
                .ForMember(b => b.FullPhoneNumber, t => t.MapFrom(a => $"{a.AreaCode} {a.PhoneNumber}"));
        }

        private string GetFullName(ApplicationUser user)
        {
            if (string.IsNullOrWhiteSpace(user.SecondName))
            {
                return $"{user.FirstName} {user.LastName}";
            }
            else
            {
                return $"{user.FirstName} {user.SecondName} {user.LastName}";
            }
        }
    }
}
