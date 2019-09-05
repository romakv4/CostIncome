using System;

namespace CostIncomeCalculator.Helpers
{
    /// <summary>
    /// IDatesHelper interface.
    /// </summary>
    public interface IDatesHelper
    {
        /// <summary>
        /// Get start week and end week dates.
        /// </summary>
        /// <param name="currentDate">DateTime</param>
        /// <returns>(Start week date, end week date)</returns>
        (DateTime, DateTime) GetWeekDateRange(DateTime currentDate);

        /// <summary>
        /// Get start month and end month dates.
        /// </summary>
        /// <param name="currentDate">DateTime</param>
        /// <returns>(Start month date, end month date)</returns>
        (DateTime, DateTime) GetMonthDateRange(DateTime currentDate);
    }
}