using e_CarRentalAPI.Models.Entities.CarDetails;
using System.ComponentModel.DataAnnotations;

namespace e_CarRentalAPI.Models.Entities
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int BodyTypeId { get; set; }
        public BodyType BodyType { get; set; }
        public int YearOfProduction { get; set; }
        public int FuelId { get; set; }
        public Fuel Fuel { get; set; }
        public string VIN { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public int Mileage { get; set; }
        public int GearboxId { get; set; }
        public Gearbox Gearbox { get; set; }
        public int PropulsionId { get; set; }
        public Propulsion Propulsion { get; set; }
        public int Capacity { get; set; }
        public int Power { get; set; }
        public int NumberOfDoors { get; set; }
        public int NumberOfSeats { get; set; }
        public bool RightSideSteeringWheel { get; set; }
        public string ? Registration { get; set; }
        public int Price { get; set; }
        public int PricePerHour { get; set; }
        public bool IsArchive { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }
}
