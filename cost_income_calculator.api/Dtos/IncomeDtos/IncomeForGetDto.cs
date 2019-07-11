using System.ComponentModel.DataAnnotations;

namespace cost_income_calculator.api.Dtos.IncomeDtos
{
    public class IncomeForGetDto
    {
        [Required]
        public string Username { get; set; }
    }
}