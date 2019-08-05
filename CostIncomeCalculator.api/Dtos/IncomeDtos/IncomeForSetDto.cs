using System;
using System.ComponentModel.DataAnnotations;

namespace cost_income_calculator.api.Dtos.IncomeDtos
{
    public class IncomeForSetDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}