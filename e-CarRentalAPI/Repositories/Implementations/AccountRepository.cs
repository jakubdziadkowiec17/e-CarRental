using AutoMapper;
using e_CarRentalAPI.Database;
using e_CarRentalAPI.Models.DTOs;
using e_CarRentalAPI.Models.Entities;
using e_CarRentalAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace e_CarRentalAPI.Repositories.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetUserAsync(string email)
        {
            var currentUser = await _userManager.FindByEmailAsync(email);

            if (currentUser == null)
            {
                return null;
            }

            return _mapper.Map<UserDTO>(currentUser);
        }

        public async Task<bool> LoginAsync(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                return result.Succeeded;
            }

            return false;
        }

        public async Task<bool> RegisterEmployeeAsync(RegisterDTO model)
        {
            var employeeRole = "Employee";
            var userExists = await _userManager.FindByNameAsync(model.Email);
            var emailExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null || emailExists != null)
            {
                return false;
            }

            var user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                PESEL = model.PESEL,
                DateOfBirth = model.DateOfBirth,
                Address = model.Address,
                AreaCode = model.AreaCode,
                PhoneNumber = model.PhoneNumber,
                PhoneNumberConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(employeeRole))
                    await _roleManager.CreateAsync(new IdentityRole(employeeRole));


                if (await _roleManager.RoleExistsAsync(employeeRole))
                {
                    await _userManager.AddToRoleAsync(user, employeeRole);
                }

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _userManager.ConfirmEmailAsync(user, token);

                return true;
            }

            return false;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> Update(ApplicationUser model)
        {
            try
            {
                var result = await _userManager.UpdateAsync(model);
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
