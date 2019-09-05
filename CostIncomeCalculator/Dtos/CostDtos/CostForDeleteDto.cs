using System.ComponentModel.DataAnnotations;

namespace cost_income_calculator.Dtos.CostDtos
{
    public class CostForDeleteDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public int[] Ids { get; set; }
    }
}