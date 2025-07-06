using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;

namespace e_CarRentalAPI.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<UserDTO> GetUserAsync(string email);
        Task<bool> LoginAsync(LoginDTO model);
        Task<bool> RegisterEmployeeAsync(RegisterDTO model);
        Task LogoutAsync();
        Task<bool> Update(ApplicationUser model);
    }
}
