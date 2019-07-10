using System;

namespace cost_income_calculator.api.Helpers
{
    public interface IDatesHelper
    {
        (DateTime, DateTime) GetWeekDateRange(DateTime currentDate);
        (DateTime, DateTime) GetMonthDateRange(DateTime date);
    }
}