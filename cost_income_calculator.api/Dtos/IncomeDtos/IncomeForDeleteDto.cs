using System.ComponentModel.DataAnnotations;

namespace cost_income_calculator.api.Dtos.IncomeDtos
{
    public class IncomeForDeleteDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public int[] Ids { get; set; }
    }
}