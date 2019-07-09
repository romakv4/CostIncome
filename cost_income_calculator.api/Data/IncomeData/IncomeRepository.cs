using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using cost_income_calculator.api.Dtos.IncomeDtos;
using cost_income_calculator.api.Models;
using Microsoft.EntityFrameworkCore;

namespace cost_income_calculator.api.Data.IncomeData
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public IncomeRepository(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<IncomeReturnDto>> GetAllIncomes(string username)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return null;

            List<Income> incomes = new List<Income>();

            incomes = await context.Incomes.ToListAsync();

            return mapper.Map<IEnumerable<IncomeReturnDto>>(incomes);
        }

        public async Task<Income> SetIncome(string username, string type, string description, double price, DateTime date)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return null;

            var income = new Income
            {
                UserId = user.Id,
                Type = type,
                Description = description,
                Price = price,
                Date = date
            };

            await context.AddAsync(income);
            await context.SaveChangesAsync();

            return income;
        }

        public async Task<Income> EditIncome(string username, int costId, string newType, string newDescription, double newPrice, DateTime newDate)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return null;

            var currentIncome = await context.Incomes.FirstOrDefaultAsync(x => x.Id == costId && x.UserId == user.Id);
            if (currentIncome == null) return null;

            currentIncome.Type = newType;
            currentIncome.Description = newDescription;
            currentIncome.Price = newPrice;

            context.Incomes.Update(currentIncome);
            await context.SaveChangesAsync();

            return currentIncome;
        }

        public async Task<Income> DeleteIncome(string username, int costId)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return null;

            var currentIncome = await context.Incomes.FirstOrDefaultAsync(x => x.Id == costId && x.UserId == user.Id);
            if (currentIncome == null) return null;

            context.Incomes.Remove(currentIncome);
            await context.SaveChangesAsync();

            return currentIncome;
        }
    }
}