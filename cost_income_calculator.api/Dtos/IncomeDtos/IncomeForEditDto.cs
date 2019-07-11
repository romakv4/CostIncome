using System;
using System.ComponentModel.DataAnnotations;

namespace cost_income_calculator.api.Dtos.IncomeDtos
{
    public class IncomeForEditDto
    {
        [Required]
        public string Username { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
    }
}