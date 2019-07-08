using System.ComponentModel.DataAnnotations;

namespace cost_income_calculator.api.Dtos.UserDtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "You must specify password between 8 and 30 characters")]
        public string Password { get; set; }
    }
}