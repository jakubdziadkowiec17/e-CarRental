using e_CarRentalAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace e_CarRentalAPI.Models.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public string Description { get; set; }
        public int Sum { get; set; }
        public DateTime PaymentDate { get; set; }
        public string UserId { get; set; }
        public string? UserFullName { get; set; }
    }
}
