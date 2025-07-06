using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace e_CarRentalAPI.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string ? SecondName { get; set; }
        public string LastName { get; set; }
        //[StringLength(11)]
        public string PESEL { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; }
        //[RegularExpression(@"\+\d{4}")]
        public string AreaCode { get; set; }
        //[StringLength(9)]
        public string PhoneNumber { get; set; }
        public bool IsArchive { get; set; }
        public ICollection<Rental> Rentals { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
