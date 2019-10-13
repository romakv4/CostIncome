using System;
using System.ComponentModel.DataAnnotations;

namespace CostIncomeCalculator.Dtos.IncomeDtos
{
    /// <summary>
    /// Data transfer object for get periodic incomes.
    /// <see cref="Data.IncomeData.IncomeRepository.GetWeeklyIncomes" />
    /// <see cref="Data.IncomeData.IncomeRepository.GetWeeklyIncomesByCategory" />
    /// <see cref="Data.IncomeData.IncomeRepository.GetMonthlyIncomes" />
    /// <see cref="Data.IncomeData.IncomeRepository.GetMonthlyIncomesByCategory" />
    /// </summary>
    public class PeriodicIncomesDto
    {
        /// <summary>
        /// Username from database. Required.
        /// </summary>
        /// <value>string</value>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Date for periodic functions work.
        /// </summary>
        /// <value>DateTime</value>
        [Required]
        public DateTime Date { get; set; }
    }
}