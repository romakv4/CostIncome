using CostIncomeCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace CostIncomeCalculator.Data
{
    /// <summary>
    /// Data context for database.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Constructor with ensure created database.
        /// </summary>
        public DataContext()
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Base constrctor.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        /// <summary>
        /// Users database set.
        /// </summary>
        /// <value>DbSet of <see cref="User" /></value>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Costs database set.
        /// </summary>
        /// <value>DbSet of <see cref="Cost" /></value>
        public DbSet<Cost> Costs { get; set; }

        /// <summary>
        /// Incomes database set.
        /// </summary>
        /// <value>DbSet of <see cref="Income" /></value>
        public DbSet<Income> Incomes { get; set; }

        /// <summary>
        /// Limits database set.
        /// </summary>
        /// <value>DbSet of <see cref="Limit" /></value>
        public DbSet<Limit> Limits { get; set; }
    }
}