using AutoMapper;
using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;
using e_CarRentalAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace e_CarRentalAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientController(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetClientCount()
        {
            try
            {
                var clientCount = await _clientRepository.GetClientCountAsync();
                return Ok(clientCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ClientListDTO>> GetClients()
        {
            try
            {
                var clients = await _clientRepository.GetClients();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> GetClient(int id)
        {
            try
            {
                var client = await _clientRepository.GetClientByIdAsync(id);
                if (client == null)
                {
                    return NotFound();
                }

                var clientDTO = _mapper.Map<ClientDTO>(client);
                return Ok(clientDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(ClientDTO clientDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var client = _mapper.Map<Client>(clientDTO);
                await _clientRepository.AddClientAsync(client);

                return CreatedAtAction(nameof(GetClients), new { id = client.Id }, client);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, ClientDTO clientDTO)
        {
            try
            {
                var client = await _clientRepository.GetClientByIdAsync(id);
                if (client == null)
                {
                    return NotFound();
                }

                _mapper.Map(clientDTO, client);
                await _clientRepository.UpdateClientAsync(client);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var client = await _clientRepository.GetClientByIdAsync(id);
                if (client == null)
                {
                    return NotFound();
                }
                client.IsArchive = true;
                await _clientRepository.DeleteClientAsync(client);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
