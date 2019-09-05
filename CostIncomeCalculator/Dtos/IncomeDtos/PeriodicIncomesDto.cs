using System;
using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.IncomeDtos
{
    public class PeriodicIncomesDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}