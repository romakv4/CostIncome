using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.IncomeDtos
{
    public class IncomeForDeleteDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public int[] Ids { get; set; }
    }
}