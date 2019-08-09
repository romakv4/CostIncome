using System;

namespace cost_income_calculator.api.Dtos.LimitDtos
{
    public class LimitReturnDto
    {
        public string Username { get; set; }

        public string Category { get; set; }

        public decimal Value { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}