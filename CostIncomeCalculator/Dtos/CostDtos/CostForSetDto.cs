using System;
using System.ComponentModel.DataAnnotations;
using CostIncomeCalculator.Dtos._DtosCustomValidators;

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
        [Range(0.01, 999999999999)]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateValidator]
        public DateTime Date { get; set; }
    }
}