using AutoMapper;
using e_CarRentalAPI.Database;
using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;
using e_CarRentalAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_CarRentalAPI.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> GetEmployeeCountAsync()
        {
            return await _context.Users.Where(u => !u.IsArchive).CountAsync();
        }

        public async Task<List<EmployeeListDTO>> GetEmployees()
        {
            var employees = await _context.Users.Where(c => !c.IsArchive).OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ThenBy(c => c.SecondName).ThenBy(c => c.PESEL).ToListAsync();
            return _mapper.Map<List<EmployeeListDTO>>(employees);
        }

        public async Task AddEmployeeAsync(ApplicationUser employee)
        {
            _context.Users.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(ApplicationUser employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationUser> GetEmployeeByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task DeleteEmployeeAsync(ApplicationUser employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
