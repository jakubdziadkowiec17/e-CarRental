using AutoMapper;
using e_CarRentalAPI.Database;
using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;
using e_CarRentalAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_CarRentalAPI.Repositories.Implementations
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClientRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> GetClientCountAsync()
        {
            return await _context.Client.Where(u => !u.IsArchive).CountAsync();
        }

        public async Task<List<ClientListDTO>> GetClients()
        {
            var clients = await _context.Client.Where(c => !c.IsArchive).OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ThenBy(c => c.SecondName).ThenBy(c => c.PESEL).ToListAsync();
            return _mapper.Map<List<ClientListDTO>>(clients);
        }

        public async Task AddClientAsync(Client client)
        {
            _context.Client.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _context.Client.FindAsync(id);
        }

        public async Task DeleteClientAsync(Client client)
        {
            //_context.Client.Remove(client);
            //await _context.SaveChangesAsync();

            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
