namespace CostIncomeCalculator.Dtos.CostDtos
{
    /// <summary>
    /// Data transfer object for return category with max sum of incomes.
    /// <see cref="Data.CostData.CostRepository.GetMaxCostsCategoryInMonth"/>
    /// </summary>
    public class MonthCostDto
    {
        /// <summary>
        /// Category of costs.
        /// </summary>
        /// <value>string</value>
        public string Category { get; set; }

        /// <summary>
        /// Costs sum.
        /// </summary>
        /// <value>decimal</value>
        public decimal CostSum { get; set; }
    }
}