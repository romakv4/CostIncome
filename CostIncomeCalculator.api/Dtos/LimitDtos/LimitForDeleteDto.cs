using System.ComponentModel.DataAnnotations;

namespace cost_income_calculator.api.Dtos.LimitDtos
{
    public class LimitForDeleteDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public int[] Ids { get; set; }
    }
}