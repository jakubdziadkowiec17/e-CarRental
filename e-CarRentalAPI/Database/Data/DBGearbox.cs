using e_CarRentalAPI.Models.Entities.CarDetails;

namespace e_CarRentalAPI.Database.Data
{
    public static class DBGearbox
    {
        public static readonly Gearbox Automatic = new Gearbox { Id = 1, Name = "Automatic" };
        public static readonly Gearbox Manual = new Gearbox { Id = 2, Name = "Manual" };
    }
}