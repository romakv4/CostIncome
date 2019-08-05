using System;
using System.ComponentModel.DataAnnotations;

namespace cost_income_calculator.api.Dtos.LimitDtos
{
    public class LimitForEditDto
    {
        [Required]
        public string Username { get; set; }
        public string Category { get; set; }
        public double Value { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}