using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;

namespace e_CarRentalAPI.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<int> GetEmployeeCountAsync();
        Task<List<EmployeeListDTO>> GetEmployees();
        Task<ApplicationUser> GetEmployeeByIdAsync(string id);
        Task AddEmployeeAsync(ApplicationUser employee);
        Task UpdateEmployeeAsync(ApplicationUser employee);
        Task DeleteEmployeeAsync(ApplicationUser employee);
    }
}
