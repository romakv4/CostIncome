using System;
using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.LimitDtos
{
    public class LimitForEditDto
    {
        [Required]
        public string Username { get; set; }
        public string Category { get; set; }
        public decimal Value { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}