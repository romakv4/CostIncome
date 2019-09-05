using System;
using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.CostDtos
{
    public class CostForSetDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}