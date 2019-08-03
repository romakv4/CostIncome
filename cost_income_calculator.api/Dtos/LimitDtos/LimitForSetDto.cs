using System;
using System.ComponentModel.DataAnnotations;

namespace cost_income_calculator.api.Dtos.LimitDtos
{
    public class LimitForSetDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }
    }
}