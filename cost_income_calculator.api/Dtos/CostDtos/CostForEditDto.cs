using System;
using System.ComponentModel.DataAnnotations;

namespace cost_income_calculator.api.Dtos.CostDtos
{
    public class CostForEditDto
    {
        [Required]
        public string Username { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
    }
}