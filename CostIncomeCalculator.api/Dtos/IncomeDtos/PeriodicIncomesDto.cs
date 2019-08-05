using System;
using System.ComponentModel.DataAnnotations;

namespace cost_income_calculator.api.Dtos.IncomeDtos
{
    public class PeriodicIncomesDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}