using e_CarRentalAPI.Models.Entities.CarDetails;

namespace e_CarRentalAPI.Database.Data
{
    public static class DBBodyType
    {
        public static readonly BodyType Sedan = new BodyType { Id = 1, Name = "Sedan" };
        public static readonly BodyType Cabriolet = new BodyType { Id = 2, Name = "Cabriolet" };
        public static readonly BodyType SUV = new BodyType { Id = 3, Name = "SUV" };
        public static readonly BodyType StationWagon = new BodyType { Id = 4, Name = "Station wagon" };
        public static readonly BodyType Compact = new BodyType { Id = 5, Name = "Compact" };
        public static readonly BodyType SmallCar = new BodyType { Id = 6, Name = "Small car" };
    }
}