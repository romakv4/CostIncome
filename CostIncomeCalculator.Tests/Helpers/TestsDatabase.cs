using System;
using Microsoft.EntityFrameworkCore;

namespace CostIncomeCalculator.Tests.Helpers
{
    public class TestsDatabase : IDisposable
    {
        public TestsDatabase()
        {
            var localDataContext = new DbContextOptionsBuilder<Data.DataContext>()
                                    .UseInMemoryDatabase(databaseName: "CostsIncomesTests")
                                    .Options;

            context = new Data.DataContext(localDataContext);

            user = new Models.User {Username="romakv4", PasswordHash="asd"};

            context.Add(user);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Remove(user);
            context.SaveChanges();
        }

        public Data.DataContext context { get; private set; }
        public Models.User user { get; private set; }
    }
}