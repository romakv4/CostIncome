namespace cost_income_calculator.api.Dtos.CostDtos
{
    public class CostReturnDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}