using System;
using System.Threading.Tasks;
using CostIncomeCalculator.Helpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CostIncomeCalculator.Tests.Helpers
{
    public class UserHelperTests : IClassFixture<UserHelperFixture>
    {
        UserHelperFixture fixture;
        public UserHelperTests(UserHelperFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task UserExistsTest()
        {
            UserHelper helper = new UserHelper(fixture.context);
            Assert.True(await helper.UserExists(fixture.user.Username));
        }

        [Fact]
        public async Task UserNotExistsTest()
        {
            UserHelper helper = new UserHelper(fixture.context);
            Assert.False(await helper.UserExists("notExistUser"));   
        }
    }
}