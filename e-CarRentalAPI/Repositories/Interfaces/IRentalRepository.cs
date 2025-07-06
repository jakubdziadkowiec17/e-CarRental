using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_CarRentalAPI.Repositories.Interfaces
{
    public interface IRentalRepository
    {
        Task<int> GetRentalCountAsync();
        Task<List<ChartDTO>> GetRentalMonthCountAsync();
        Task<List<Rental>> GetRentals();
        Task<List<Rental>> GetRentalsByClientId(int clientId);
        Task<List<Rental>> GetRentalsByCarId(int carId);
        Task<Rental> GetRentalByIdAsync(int id);
        Task AddRentalAsync(Rental rental);
        Task UpdateRentalAsync(Rental rental);
        Task DeleteRentalAsync(Rental rental);
    }
}
