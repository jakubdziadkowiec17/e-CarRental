using System.ComponentModel.DataAnnotations;

namespace e_CarRentalAPI.Models.Entities
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public string CreatedByUser { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        //[Range(0, int.MaxValue)]
        public int Hours { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ? EndDate { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
