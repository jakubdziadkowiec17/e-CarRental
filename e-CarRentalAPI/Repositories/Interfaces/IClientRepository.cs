using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_CarRentalAPI.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<int> GetClientCountAsync();
        Task<List<ClientListDTO>> GetClients();
        Task<Client> GetClientByIdAsync(int id);
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(Client client);
    }
}
