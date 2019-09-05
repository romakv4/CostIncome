using CostIncomeCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace CostIncomeCalculator.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            Database.EnsureCreated();
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Limit> Limits { get; set; }
    }
}