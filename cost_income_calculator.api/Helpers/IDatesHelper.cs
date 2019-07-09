using System;

namespace cost_income_calculator.api.Helpers
{
    public interface IDatesHelper
    {
        (DateTime, DateTime) GetFirstAndLastDateOfMonth(DateTime date);
    }
}