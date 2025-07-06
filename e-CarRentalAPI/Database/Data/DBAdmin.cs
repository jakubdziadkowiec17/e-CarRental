using e_CarRentalAPI.Models.Entities;

namespace e_CarRentalAPI.Database.Data
{
    public static class DBAdmin
    {
        public static readonly ApplicationUser Account = new ApplicationUser
        {
            Id = "1111-1111-1111-1111-111111111111",
            Email = "Admin@Admin.pl",
            NormalizedEmail = "ADMIN@ADMIN.PL",
            EmailConfirmed = true,
            UserName = "Admin@Admin.pl",
            NormalizedUserName = "ADMIN@ADMIN.PL",
            PhoneNumberConfirmed = true,
            FirstName = "Application",
            LastName = "Admin",
            PESEL = "11111111111",
            DateOfBirth = new DateOnly(2000, 01, 01),
            Address = "Poland",
            AreaCode = "+48",
            PhoneNumber = "123456789",
            IsArchive = false
        };
        public static readonly string Password = "Admin11!";
    }
}
