using System;

namespace cost_income_calculator.api.Helpers
{
    public class DatesHelper : IDatesHelper
    {
        public (DateTime, DateTime) GetFirstAndLastDateOfMonth(DateTime currentDate)
        {
            var firstDateOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);
            return (firstDateOfMonth, lastDateOfMonth);
        }
    }
}