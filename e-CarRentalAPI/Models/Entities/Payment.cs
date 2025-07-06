using System.ComponentModel.DataAnnotations;

namespace e_CarRentalAPI.Models.Entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
        public string Description { get; set; }
        //[Range(0, int.MaxValue)]
        public int Sum { get; set; }
        public DateTime PaymentDate { get; set; }
        public string CreatedByUser { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
