using System.ComponentModel.DataAnnotations;

namespace e_CarRentalAPI.Models.DTOs
{
    public class ChartDTO
    {
        [Required]
        public string MonthWithYear { get; set; }
        [Required]
        public int MonthCount { get; set; }
    }
}
