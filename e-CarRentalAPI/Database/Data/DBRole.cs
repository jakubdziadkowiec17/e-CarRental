using e_CarRentalAPI.Constants;
using Microsoft.AspNetCore.Identity;

namespace e_CarRentalAPI.Database.Data
{
    public static class DBRole
    {
        public static readonly IdentityRole Admin = new IdentityRole { Id = "11111111-1111-1111-1111-111111111111", Name = Role.Admin, NormalizedName = Role.Admin.ToUpper() };
        public static readonly IdentityRole Employee = new IdentityRole { Id = "22222222-2222-2222-2222-222222222222", Name = Role.Employee, NormalizedName = Role.Employee.ToUpper() };
    }
}
