using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.UserDtos
{
    /// <summary>
    /// Data transfer object for new user registration.
    /// </summary>
    public class UserForRegisterDto
    {
        /// <summary>
        /// User email. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "You must specify password between 8 and 30 characters")]
        public string Password { get; set; }
    }
}