namespace CostIncomeCalculator.Dtos.IncomeDtos
{
    /// <summary>
    /// Data transfer object for return category with max sum of incomes.
    /// <see cref="Data.IncomeData.IncomeRepository.GetMaxIncomesCategoryInMonth"/>
    /// </summary>
    public class MonthIncomeDto
    {
        /// <summary>
        /// Category of incomes.
        /// </summary>
        /// <value>string</value>
        public string Category { get; set; }

        /// <summary>
        /// Incomes sum.
        /// </summary>
        /// <value>decimal</value>
        public decimal IncomeSum { get; set; }
    }
}