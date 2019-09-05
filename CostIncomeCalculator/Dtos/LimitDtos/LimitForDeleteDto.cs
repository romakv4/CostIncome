using System.ComponentModel.DataAnnotations;

namespace cost_income_calculator.Dtos.LimitDtos
{
    public class LimitForDeleteDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public int[] Ids { get; set; }
    }
}