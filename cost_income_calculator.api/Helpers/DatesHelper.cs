using System;

namespace cost_income_calculator.api.Helpers
{
    public class DatesHelper : IDatesHelper
    {
        public (DateTime, DateTime) GetWeekDateRange(DateTime currentDate)
        {
            int days = currentDate.DayOfWeek - DayOfWeek.Monday;
            var firstDateOfWeek = currentDate.AddDays(-days);
            var lastDayOfWeek = firstDateOfWeek.AddDays(6);
            return (firstDateOfWeek, lastDayOfWeek);
        }
        public (DateTime, DateTime) GetMonthDateRange(DateTime currentDate)
        {
            var firstDateOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);
            return (firstDateOfMonth, lastDateOfMonth);
        }
    }
}