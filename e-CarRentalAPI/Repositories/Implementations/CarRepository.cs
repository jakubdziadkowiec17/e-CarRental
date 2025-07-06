using AutoMapper;
using e_CarRentalAPI.Database;
using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;
using e_CarRentalAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_CarRentalAPI.Repositories.Implementations
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CarRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> GetCarCountAsync()
        {
            return await _context.Car.Where(u => !u.IsArchive).CountAsync();
        }

        public async Task<List<CarListDTO>> GetCars()
        {
            var Cars = await _context.Car.Where(c => !c.IsArchive).OrderBy(c => c.Brand).ThenBy(c => c.Model).ThenBy(c => c.YearOfProduction).ToListAsync();
            return _mapper.Map<List<CarListDTO>>(Cars);
        }

        public async Task AddCarAsync(Car car)
        {
            _context.Car.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCarAsync(Car car)
        {
            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _context.Car.FindAsync(id);
        }

        public async Task DeleteCarAsync(Car car)
        {
            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
