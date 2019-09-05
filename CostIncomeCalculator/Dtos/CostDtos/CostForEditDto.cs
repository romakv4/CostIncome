using System;
using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.CostDtos
{
    public class CostForEditDto
    {
        [Required]
        public string Username { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}