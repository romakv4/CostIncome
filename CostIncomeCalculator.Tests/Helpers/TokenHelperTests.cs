using CostIncomeCalculator.Helpers;
using CostIncomeCalculator.Models;
using Xunit;

namespace CostIncomeCalculator.Tests.Helpers
{
    public class TokenHelperTests
    {

        [Fact]
        public void GenerateTokenTest()
        {
            TokenHelper tokenHelper = new TokenHelper();
            var user = new User { Id = 3, Username = "romakv4" };
            string token = tokenHelper.GenerateToken(user, "super puper secret");
            Assert.NotNull(token);
            Assert.True(token.Length != 0);
        }
    }
}