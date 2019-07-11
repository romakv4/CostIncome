using System;
using System.ComponentModel.DataAnnotations;

namespace cost_income_calculator.api.Dtos.CostDtos
{
    public class CostForSetDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}