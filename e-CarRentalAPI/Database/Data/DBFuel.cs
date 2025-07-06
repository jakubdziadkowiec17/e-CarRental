using e_CarRentalAPI.Models.Entities.CarDetails;

namespace e_CarRentalAPI.Database.Data
{
    public static class DBFuel
    {
        public static readonly Fuel Hybrid = new Fuel { Id = 1, Name = "Hybrid" };
        public static readonly Fuel Gas = new Fuel { Id = 2, Name = "Gas" };
        public static readonly Fuel Diesel = new Fuel { Id = 3, Name = "Diesel" };
        public static readonly Fuel Electric = new Fuel { Id = 4, Name = "Electric" };
        public static readonly Fuel Hydrogen = new Fuel { Id = 5, Name = "Hydrogen" };
    }
}