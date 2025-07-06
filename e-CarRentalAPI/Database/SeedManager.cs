using e_CarRentalAPI.Database;
using e_CarRentalAPI.Database.Data;
using e_CarRentalAPI.Models.Entities;
using e_CarRentalAPI.Models.Entities.CarDetails;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Soccerity_API.Database
{
    public static class SeedManager
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await CreateBodyType(context);
            await CreateColor(context);
            await CreateFuel(context);
            await CreateGearbox(context);
            await CreatePropulsion(context);
            await CreateRoles(roleManager);
            await CreateAdminUser(userManager);
        }

        public static async Task CreateBodyType(ApplicationDbContext context)
        {
            var bodyTypes = new List<BodyType>
            {
                DBBodyType.Sedan,
                DBBodyType.Cabriolet,
                DBBodyType.SUV,
                DBBodyType.StationWagon,
                DBBodyType.Compact,
                DBBodyType.SmallCar
            };

            foreach (var bodyType in bodyTypes)
            {
                if (!await context.BodyType.AnyAsync(bt => bt.Id == bodyType.Id))
                {
                    await context.BodyType.AddAsync(bodyType);
                }
            }

            await context.SaveChangesAsync();
        }

        public static async Task CreateColor(ApplicationDbContext context)
        {
            var colors = new List<Color>
            {
                DBColor.White, DBColor.Black, DBColor.Blue, DBColor.Green, DBColor.Yellow,
                DBColor.Orange, DBColor.Purple, DBColor.Brown, DBColor.Gray, DBColor.Pink,
                DBColor.Red, DBColor.Gold, DBColor.Silver, DBColor.Beige, DBColor.NavyBlue, DBColor.Lime
            };

            foreach (var color in colors)
            {
                if (!await context.Color.AnyAsync(c => c.Id == color.Id))
                {
                    await context.Color.AddAsync(color);
                }
            }

            await context.SaveChangesAsync();
        }

        public static async Task CreateFuel(ApplicationDbContext context)
        {
            var fuels = new List<Fuel>
            {
                DBFuel.Hybrid,
                DBFuel.Gas,
                DBFuel.Diesel,
                DBFuel.Electric,
                DBFuel.Hydrogen
            };

            foreach (var fuel in fuels)
            {
                if (!await context.Fuel.AnyAsync(f => f.Id == fuel.Id))
                {
                    await context.Fuel.AddAsync(fuel);
                }
            }

            await context.SaveChangesAsync();
        }

        public static async Task CreateGearbox(ApplicationDbContext context)
        {
            var gearboxes = new List<Gearbox>
            {
                DBGearbox.Automatic,
                DBGearbox.Manual
            };

            foreach (var gearbox in gearboxes)
            {
                if (!await context.Gearbox.AnyAsync(g => g.Id == gearbox.Id))
                {
                    await context.Gearbox.AddAsync(gearbox);
                }
            }

            await context.SaveChangesAsync();
        }

        public static async Task CreatePropulsion(ApplicationDbContext context)
        {
            var propulsions = new List<Propulsion>
            {
                DBPropulsion.FrontWheels,
                DBPropulsion.RearWheels,
                DBPropulsion.FourXFour
            };

            foreach (var propulsion in propulsions)
            {
                if (!await context.Propulsion.AnyAsync(p => p.Id == propulsion.Id))
                {
                    await context.Propulsion.AddAsync(propulsion);
                }
            }

            await context.SaveChangesAsync();
        }

        public static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            List<IdentityRole> roles = [DBRole.Admin, DBRole.Employee];

            foreach (var role in roles)
            {
                if (await roleManager.FindByIdAsync(role.Id) is null)
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }

        public static async Task CreateAdminUser(UserManager<ApplicationUser> userManager)
        {
            if (await userManager.FindByIdAsync(DBAdmin.Account.Id) is null)
            {
                await userManager.CreateAsync(DBAdmin.Account, DBAdmin.Password);
                await userManager.AddToRoleAsync(DBAdmin.Account, DBRole.Admin.Name);
            }
        }
    }
}