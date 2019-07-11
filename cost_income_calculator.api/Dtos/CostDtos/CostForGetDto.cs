using System.ComponentModel.DataAnnotations;

namespace cost_income_calculator.api.Dtos.CostDtos
{
    public class CostForGetDto
    {
        [Required]
        public string Username { get; set; }
    }
}