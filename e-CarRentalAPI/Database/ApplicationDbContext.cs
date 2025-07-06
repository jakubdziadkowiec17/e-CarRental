using e_CarRentalAPI.Models.Entities;
using e_CarRentalAPI.Models.Entities.CarDetails;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_CarRentalAPI.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Rental> Rental { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<BodyType> BodyType { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Fuel> Fuel { get; set; }
        public DbSet<Gearbox> Gearbox { get; set; }
        public DbSet<Propulsion> Propulsion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BodyType>().Property(a => a.Id).ValueGeneratedNever();
            modelBuilder.Entity<Color>().Property(a => a.Id).ValueGeneratedNever();
            modelBuilder.Entity<Fuel>().Property(a => a.Id).ValueGeneratedNever();
            modelBuilder.Entity<Gearbox>().Property(a => a.Id).ValueGeneratedNever();
            modelBuilder.Entity<Propulsion>().Property(a => a.Id).ValueGeneratedNever();

            modelBuilder.Entity<Payment>()
                .HasOne(a => a.Rental)
                .WithMany(b => b.Payments)
                .HasForeignKey(a => a.RentalId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(a => a.ApplicationUser)
                .WithMany(b => b.Payments)
                .HasForeignKey(a => a.CreatedByUser);

            modelBuilder.Entity<Rental>()
                .HasOne(a => a.ApplicationUser)
                .WithMany(b => b.Rentals)
                .HasForeignKey(a => a.CreatedByUser);

            modelBuilder.Entity<Rental>()
                .HasOne(a => a.Client)
                .WithMany(b => b.Rentals)
                .HasForeignKey(a => a.ClientId);

            modelBuilder.Entity<Rental>()
                .HasOne(a => a.Car)
                .WithMany(b => b.Rentals)
                .HasForeignKey(a => a.CarId);

            modelBuilder.Entity<Car>()
                .HasOne(a => a.BodyType)
                .WithMany(b => b.Cars)
                .HasForeignKey(a => a.BodyTypeId);

            modelBuilder.Entity<Car>()
                .HasOne(a => a.Fuel)
                .WithMany(b => b.Cars)
                .HasForeignKey(a => a.FuelId);

            modelBuilder.Entity<Car>()
                .HasOne(a => a.Color)
                .WithMany(b => b.Cars)
                .HasForeignKey(a => a.ColorId);

            modelBuilder.Entity<Car>()
                .HasOne(a => a.Gearbox)
                .WithMany(b => b.Cars)
                .HasForeignKey(a => a.GearboxId);

            modelBuilder.Entity<Car>()
                .HasOne(a => a.Propulsion)
                .WithMany(b => b.Cars)
                .HasForeignKey(a => a.PropulsionId);
        }
    }
}
