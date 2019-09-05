using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.LimitDtos
{
    public class LimitForDeleteDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public int[] Ids { get; set; }
    }
}