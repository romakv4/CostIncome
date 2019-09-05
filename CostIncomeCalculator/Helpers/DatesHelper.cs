using System;

namespace cost_income_calculator.Helpers
{   
    /// <summary>
    /// DatesHelper class.
    /// Contains methods to simply work with dates.
    /// </summary>
    public class DatesHelper : IDatesHelper
    {
        /// <summary>
        /// Get start week and end week dates.
        /// </summary>
        /// <param name="currentDate">DateTime</param>
        /// <returns>(Start week date, end week date)</returns>
        public (DateTime, DateTime) GetWeekDateRange(DateTime currentDate)
        {
            int days = currentDate.DayOfWeek - DayOfWeek.Monday;
            var firstDateOfWeek = currentDate.AddDays(-days);
            var lastDayOfWeek = firstDateOfWeek.AddDays(6);
            return (firstDateOfWeek, lastDayOfWeek);
        }

        /// <summary>
        /// Get start month and end months dates.
        /// </summary>
        /// <param name="currentDate">DateTime</param>
        /// <returns>(Start month date, end month date)</returns>
        public (DateTime, DateTime) GetMonthDateRange(DateTime currentDate)
        {
            var firstDateOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);
            return (firstDateOfMonth, lastDateOfMonth);
        }
    }
}