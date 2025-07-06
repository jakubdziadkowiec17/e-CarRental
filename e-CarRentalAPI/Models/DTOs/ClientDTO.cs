using System.ComponentModel.DataAnnotations;

namespace e_CarRentalAPI.Models.DTOs
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PESEL { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; }
        public string AreaCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
