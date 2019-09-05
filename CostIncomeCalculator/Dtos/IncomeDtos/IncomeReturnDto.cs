using System;

namespace CostIncomeCalculator.Dtos.IncomeDtos
{
    public class IncomeReturnDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}