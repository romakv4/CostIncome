using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.CostDtos
{
    public class CostForDeleteDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(1)]
        public int[] Ids { get; set; }
    }
}