using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;

namespace e_CarRentalAPI.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<int> GetCarCountAsync();
        Task<List<CarListDTO>> GetCars();
        Task<Car> GetCarByIdAsync(int id);
        Task AddCarAsync(Car car);
        Task UpdateCarAsync(Car car);
        Task DeleteCarAsync(Car car);
    }
}
