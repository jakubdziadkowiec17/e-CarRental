using AutoMapper;
using e_CarRentalAPI.Database;
using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;
using e_CarRentalAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace e_CarRentalAPI.Repositories.Implementations
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RentalRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> GetRentalCountAsync()
        {
            return await _context.Rental.CountAsync();
        }

        public async Task<List<ChartDTO>> GetRentalMonthCountAsync()
        {
            var endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var startDate = endDate.AddMonths(-11);

            var monthlyCounts = Enumerable.Range(0, 12)
                .Select(offset => new
                {
                    MonthWithYear = startDate.AddMonths(offset),
                    MonthCount = _context.Rental
                        .Count(rental => rental.StartDate.Year == startDate.AddMonths(offset).Year && rental.StartDate.Month == startDate.AddMonths(offset).Month)
                })
                .Select(item => new ChartDTO
                {
                    MonthWithYear = item.MonthWithYear.ToString("MM.yyyy"),
                    MonthCount = item.MonthCount
                })
                .ToList();

            return monthlyCounts;
        }

        public async Task<List<Rental>> GetRentals()
        {
            var rentals = await _context.Rental.OrderByDescending(c => c.EndDate).ToListAsync();
            return rentals;
        }

        public async Task<List<Rental>> GetRentalsByClientId(int clientId)
        {
            var rentals = await _context.Rental.Where(a => a.ClientId == clientId).OrderByDescending(c => c.EndDate).ToListAsync();
            return rentals;
        }

        public async Task<List<Rental>> GetRentalsByCarId(int carId)
        {
            var rentals = await _context.Rental.Where(a => a.CarId == carId).OrderByDescending(c => c.EndDate).ToListAsync();
            return rentals;
        }

        public async Task AddRentalAsync(Rental rental)
        {
            _context.Rental.Add(rental);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRentalAsync(Rental rental)
        {
            _context.Entry(rental).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Rental> GetRentalByIdAsync(int id)
        {
            return await _context.Rental.FindAsync(id);
        }

        public async Task DeleteRentalAsync(Rental rental)
        {
            _context.Rental.Remove(rental);
            await _context.SaveChangesAsync();
        }
    }
}
