namespace cost_income_calculator.api.Dtos.IncomeDtos
{
    public class ManyIncomesForDeleteDto
    {
        public string Username { get; set; }
        public int[] Ids { get; set; }
    }
}