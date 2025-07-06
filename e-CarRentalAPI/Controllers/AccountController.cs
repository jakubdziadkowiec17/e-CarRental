using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using e_CarRentalAPI.Repositories.Interfaces;

namespace e_CarRentalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IAccountRepository accountRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _accountRepository = accountRepository;
            _roleManager = roleManager;
        }

        [Authorize]
        [HttpGet("getCurrentUser")]
        public async Task<IActionResult> GetCurrentUser(string email)
        {
            var user = await _accountRepository.GetUserAsync(email);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }

        [Authorize]
        [HttpGet("isAdmin")]
        public async Task<ActionResult<bool>> isAdmin(string email)
        {
            var adminRole = "Admin";
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            if (await _roleManager.RoleExistsAsync(adminRole))
            {
                var isAdmin = await _userManager.IsInRoleAsync(user, adminRole);

                if (isAdmin)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var result = await _accountRepository.LoginAsync(model);

            if (result)
            {
                var authClaims = new List<Claim>
                    {
                        new(ClaimTypes.Email, model.Email),
                        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(10),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                var newResult = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(newResult);
            }

            return BadRequest();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.LogoutAsync();
            return Ok();
        }

        [Authorize]
        [HttpPut("editProfile")]
        public async Task<IActionResult> EditProfile(string email, [FromBody] UserDTO model)
        {
            var currentUser = await _userManager.FindByEmailAsync(email);

            if (currentUser != null || email == model.Email)
            {
                currentUser.FirstName = model.FirstName;
                currentUser.SecondName = model.SecondName;
                currentUser.LastName = model.LastName;
                currentUser.Email = model.Email;
                currentUser.PESEL = model.PESEL;
                currentUser.DateOfBirth = model.DateOfBirth;
                currentUser.Address = model.Address;
                currentUser.AreaCode = model.AreaCode;
                currentUser.PhoneNumber = model.PhoneNumber;

                var result = await _accountRepository.Update(currentUser);

                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPut("resetPassword")]
        public async Task<IActionResult> ResetPassword(string email, [FromBody] ResetPasswordDTO model)
        {
            var currentUser = await _userManager.FindByEmailAsync(email);
            if (currentUser != null)
            {
                var isOldPasswordCorrect = await _userManager.CheckPasswordAsync(currentUser, model.OldPassword);

                if (isOldPasswordCorrect)
                {
                    if (model.Password == model.ConfirmPassword)
                    {
                        var passwordHash = _userManager.PasswordHasher.HashPassword(currentUser, model.Password);
                        currentUser.PasswordHash = passwordHash;

                        var result = await _accountRepository.Update(currentUser);

                        if (result)
                        {
                            return Ok();
                        }
                    }
                }
            }
            return BadRequest();
        }
    }
}
