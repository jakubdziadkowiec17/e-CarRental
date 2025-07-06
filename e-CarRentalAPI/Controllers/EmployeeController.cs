using AutoMapper;
using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;
using e_CarRentalAPI.Repositories.Implementations;
using e_CarRentalAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace e_CarRentalAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeController(IEmployeeRepository employeeRepository, IAccountRepository accountRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _accountRepository = accountRepository;
            _userManager = userManager;
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetEmployeeCount()
        {
            try
            {
                var employeeCount = await _employeeRepository.GetEmployeeCountAsync();
                return Ok(employeeCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<ActionResult<EmployeeListDTO>> GetEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetEmployee(string id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }

                var userDTO = _mapper.Map<UserDTO>(employee);
                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmployee([FromBody] RegisterDTO model)
        {
            try
            {
                var result = await _accountRepository.RegisterEmployeeAsync(model);

                if (result)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, UserDTO userDTO)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }

                _mapper.Map(userDTO, employee);
                await _employeeRepository.UpdateEmployeeAsync(employee);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                employee.IsArchive = true;
                await _employeeRepository.DeleteEmployeeAsync(employee);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPut("resetPasswordForEmployee")]
        public async Task<IActionResult> resetPasswordForEmployee(string id, [FromBody] ResetPasswordAdminDTO model)
        {
            var currentUser = await _userManager.FindByIdAsync(id);
            if (currentUser != null)
            {
                if (model.Password == model.ConfirmPassword)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(currentUser);
                    var result = await _userManager.ResetPasswordAsync(currentUser, token, model.Password);

                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }
            }
            return BadRequest();
        }
    }
}
