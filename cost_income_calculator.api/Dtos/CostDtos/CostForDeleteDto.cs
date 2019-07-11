using System.ComponentModel.DataAnnotations;

namespace cost_income_calculator.api.Dtos.CostDtos
{
    public class CostForDeleteDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public int[] Ids { get; set; }
    }
}