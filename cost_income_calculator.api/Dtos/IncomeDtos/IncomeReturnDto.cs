using System;

namespace cost_income_calculator.api.Dtos.IncomeDtos
{
    public class IncomeReturnDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
    }
}