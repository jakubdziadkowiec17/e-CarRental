using e_CarRentalAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace e_CarRentalAPI.Models.DTOs
{
    public class RentalDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string? ClientFullName { get; set; }
        public string UserId { get; set; }
        public string? UserFullName { get; set; }
        public int CarId { get; set; }
        public string? FullNameWithYear { get; set; }
        public int Hours { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ? IsPaid { get; set; }
        public string ? PaymentStatus { get; set; }
    }
}
