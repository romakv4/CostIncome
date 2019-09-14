using System;

namespace CostIncomeCalculator.Dtos.IncomeDtos
{
    /// <summary>
    /// Data transfer object for return income.
    /// <see cref="Data.IncomeData.IncomeRepository.GetAllIncomes"/>
    /// <see cref="Data.IncomeData.IncomeRepository.GetConcreteIncome"/>
    /// <see cref="Data.IncomeData.IncomeRepository.GetWeeklyIncomes"/>
    /// <see cref="Data.IncomeData.IncomeRepository.GetWeeklyIncomesByCategory"/>
    /// <see cref="Data.IncomeData.IncomeRepository.GetMonthlyIncomes"/>
    /// <see cref="Data.IncomeData.IncomeRepository.GetMonthlyIncomesByCategory"/>
    /// </summary>
    public class IncomeReturnDto
    {
        /// <summary>
        /// Unique identificator of income in database.
        /// </summary>
        /// <value>int</value>
        public int Id { get; set; }

        /// <summary>
        /// Category of income.
        /// </summary>
        /// <value>string</value>
        public string Category { get; set; }

        /// <summary>
        /// Description of income.
        /// </summary>
        /// <value>string</value>
        public string Description { get; set; }

        /// <summary>
        /// Price of income.
        /// </summary>
        /// <value>decimal</value>
        public decimal Price { get; set; }

        /// <summary>
        /// Date of income.
        /// </summary>
        /// <value>DateTime</value>
        public DateTime Date { get; set; }
    }
}