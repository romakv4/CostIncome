using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace cost_income_calculator.api.Dtos.CostDtos
{
    public class PeriodicCostsDto
    {
        [Required]
        public string Username { get;set; }
        [Required]
        public DateTime Date { get; set; }
    }
}