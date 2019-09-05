using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CostIncomeCalculator.Helpers;

namespace CostIncomeCalculator.Tests.Helpers
{
    public class DatesHelperTests
    {

        [Fact]
        public void GetWeekDateRangeTest()
        {
            DatesHelper datesHelper = new DatesHelper();
            Assert.Equal((new DateTime(2019, 9, 2), new DateTime(2019, 9, 8)), datesHelper.GetWeekDateRange(new DateTime(2019, 9, 5)));
        }

        [Fact]
        public void GetMonthDateRangeTest()
        {
            DatesHelper datesHelper = new DatesHelper();
            Assert.Equal((new DateTime(2019, 9, 1), new DateTime(2019, 9, 30)), datesHelper.GetMonthDateRange(new DateTime(2019, 9, 5)));
        }
    }
}
