using System.ComponentModel.DataAnnotations;

namespace e_CarRentalAPI.Models.Entities.CarDetails
{
    public class Propulsion
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
