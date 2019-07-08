namespace cost_income_calculator.api.Dtos
{
    public class CostDto
    {
        public int UserId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}