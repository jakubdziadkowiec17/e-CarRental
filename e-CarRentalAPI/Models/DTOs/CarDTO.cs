using e_CarRentalAPI.Models.Entities.CarDetails;
using e_CarRentalAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace e_CarRentalAPI.Models.DTOs
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int BodyTypeId { get; set; }
        public int YearOfProduction { get; set; }
        public int FuelId { get; set; }
        public string VIN { get; set; }
        public int ColorId { get; set; }
        public int Mileage { get; set; }
        public int GearboxId { get; set; }
        public int PropulsionId { get; set; }
        public int Capacity { get; set; }
        public int Power { get; set; }
        public int NumberOfDoors { get; set; }
        public int NumberOfSeats { get; set; }
        public bool RightSideSteeringWheel { get; set; }
        public string? Registration { get; set; }
        public int Price { get; set; }
        public int PricePerHour { get; set; }
    }
}
