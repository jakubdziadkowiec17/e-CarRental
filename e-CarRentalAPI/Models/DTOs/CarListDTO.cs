using e_CarRentalAPI.Models.Entities.CarDetails;
using e_CarRentalAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace e_CarRentalAPI.Models.DTOs
{
    public class CarListDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int BodyTypeId { get; set; }
        public int YearOfProduction { get; set; }
        public string VIN { get; set; }
        public string? Registration { get; set; }
        public int Price { get; set; }
        public int PricePerHour { get; set; }
    }
}
