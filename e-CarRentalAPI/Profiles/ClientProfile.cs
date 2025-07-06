using AutoMapper;
using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;

namespace e_CarRentalAPI.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientListDTO>()
                .ForMember(b => b.FullName, t => t.MapFrom(a => GetFullName(a)))
                .ForMember(b => b.FullPhoneNumber, t => t.MapFrom(a => $"{a.AreaCode} {a.PhoneNumber}"));

            CreateMap<ClientDTO, Client>().ReverseMap();
        }

        private string GetFullName(Client client)
        {
            if (string.IsNullOrWhiteSpace(client.SecondName))
            {
                return $"{client.FirstName} {client.LastName}";
            }
            else
            {
                return $"{client.FirstName} {client.SecondName} {client.LastName}";
            }
        }
    }
}
