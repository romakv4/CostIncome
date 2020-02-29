using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.UserDtos
{
    /// <summary>
    /// Data transfer object for reset user password.
    /// </summary>
    public class UserForResetPasswordDto
    {
        /// <summary>
        /// User email. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}