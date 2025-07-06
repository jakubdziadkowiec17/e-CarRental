using System.ComponentModel.DataAnnotations;

namespace e_CarRentalAPI.Models.DTOs
{
    public class ClientListDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PESEL { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; }
        public string FullPhoneNumber { get; set; }
    }
}
