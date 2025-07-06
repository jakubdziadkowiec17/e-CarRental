using e_CarRentalAPI.Models.Entities.CarDetails;

namespace e_CarRentalAPI.Database.Data
{
    public static class DBPropulsion
    {
        public static readonly Propulsion FrontWheels = new Propulsion { Id = 1, Name = "Front wheels" };
        public static readonly Propulsion RearWheels = new Propulsion { Id = 2, Name = "Rear wheels" };
        public static readonly Propulsion FourXFour = new Propulsion { Id = 3, Name = "4x4" };
    }
}