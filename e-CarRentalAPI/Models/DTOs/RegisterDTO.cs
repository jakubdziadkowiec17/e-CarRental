using System.ComponentModel.DataAnnotations;

namespace e_CarRentalAPI.Models.DTOs
{
    public class RegisterDTO
    {
        //[Required]
        public string FirstName { get; set; }
        public string? SecondName { get; set; }
        //[Required]
        public string LastName { get; set; }
        //[Required]
        //[EmailAddress]
        public string Email { get; set; }
        //[Required]
        public string Password { get; set; }
        //[Required]
        //[Compare("Password")]
        public string ConfirmPassword { get; set; }
        //[Required]
        //[StringLength(11)]
        public string PESEL { get; set; }
        //[Required]
        public DateOnly DateOfBirth { get; set; }
        //[Required]
        public string Address { get; set; }
        //[Required]
        //[RegularExpression(@"\+\d{4}")]
        public string AreaCode { get; set; }
        //[Required]
        //[StringLength(9)]
        public string PhoneNumber { get; set; }
    }
}
