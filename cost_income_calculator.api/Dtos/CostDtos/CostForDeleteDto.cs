namespace cost_income_calculator.api.Dtos.CostDtos
{
    public class ManyCostsForDeleteDto
    {
        public string Username { get; set; }
        public int[] Ids { get; set; }
    }
}