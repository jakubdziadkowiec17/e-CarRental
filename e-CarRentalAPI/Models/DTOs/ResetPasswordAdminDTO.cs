using System.ComponentModel.DataAnnotations;

namespace e_CarRentalAPI.Models.DTOs
{
    public class ResetPasswordAdminDTO
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
