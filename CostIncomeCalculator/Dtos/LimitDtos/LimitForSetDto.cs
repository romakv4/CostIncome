using System;
using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.LimitDtos
{
    public class LimitForSetDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }
    }
}