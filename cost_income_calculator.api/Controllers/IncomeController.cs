using System.Threading.Tasks;
using cost_income_calculator.api.Data.IncomeData;
using cost_income_calculator.api.Dtos.IncomeDtos;
using cost_income_calculator.api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace cost_income_calculator.api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeRepository repository;
        private readonly IConfiguration config;
        private readonly IUserHelper userHelper;
        public IncomeController(IIncomeRepository repository, IConfiguration config, IUserHelper userHelper)
        {
            this.userHelper = userHelper;
            this.config = config;
            this.repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllIncomes(IncomeForGetDto incomeForGetDto)
        {
            try
            {
                if (!await userHelper.UserExists(incomeForGetDto.Username))
                    return BadRequest("This username doesn't exists");
                
                var incomes = await repository.GetAllIncomes(incomeForGetDto);
                
                return Ok(incomes);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("weekly")]
        public async Task<IActionResult> GetWeeklyIncomes(PeriodicIncomesDto periodicIncomesDto)
        {
            try
            {
                if (!await userHelper.UserExists(periodicIncomesDto.Username))
                    return BadRequest("This username doesn't exists");
                
                var weeklyIncomes = await repository.GetWeeklyIncomes(periodicIncomesDto);
                
                return Ok(weeklyIncomes);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("weekly/{category}")]
        public async Task<IActionResult> GetWeeklyIncomesByCategory(PeriodicIncomesDto periodicIncomesDto, string category)
        {
            try
            {
                if (!await userHelper.UserExists(periodicIncomesDto.Username))
                    return BadRequest("This username doesn't exists");
                
                var weeklyIncomesByCategory = await repository.GetWeeklyIncomesByCategory(periodicIncomesDto, category);
                
                return Ok(weeklyIncomesByCategory);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("monthly")]
        public async Task<IActionResult> GetMonthlyIncomes(PeriodicIncomesDto periodicIncomesDto)
        {
            try
            {
                if (!await userHelper.UserExists(periodicIncomesDto.Username))
                    return BadRequest("This username doesn't exists");
                
                var monthlyIncomes = await repository.GetMonthlyIncomes(periodicIncomesDto);
                
                return Ok(monthlyIncomes);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("monthly/{category}")]
        public async Task<IActionResult> GetMonthlyIncomesByCategory(PeriodicIncomesDto periodicIncomesDto, string category)
        {
            try
            {
                if (!await userHelper.UserExists(periodicIncomesDto.Username))
                    return BadRequest("This username doesn't exists");
                
                var monthlyIncomesByCategory = await repository.GetMonthlyIncomesByCategory(periodicIncomesDto, category);
                
                return Ok(monthlyIncomesByCategory);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("monthly/max")]
        public async Task<IActionResult> GetMaxMonthlyIncomes(PeriodicIncomesDto periodicIncomesDto)
        {
            try
            {
                if (!await userHelper.UserExists(periodicIncomesDto.Username))
                    return BadRequest("This username doesn't exists");
                
                var maxMonthlyIncomes = await repository.GetMaxIncomesCategoryInMonth(periodicIncomesDto);
                
                return Ok(maxMonthlyIncomes);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("set")]
        public async Task<IActionResult> SetIncome(IncomeForSetDto incomeForSetDto)
        {
            try
            {
                if (!await userHelper.UserExists(incomeForSetDto.Username))
                    return BadRequest("This username doesn't exists");

                var settedIncome = await repository.SetIncome(incomeForSetDto);
                
                return StatusCode(201);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditIncome(int id, IncomeForEditDto incomeForEditDto)
        {
            try
            {
                if (!await userHelper.UserExists(incomeForEditDto.Username))
                    return BadRequest("This username doesn't exists");
                
                var editedIncome = await repository.EditIncome(id, incomeForEditDto);

                return StatusCode(204);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteIncomes(IncomeForDeleteDto incomeForDeleteDto)
        {
            try
            {
                if (!await userHelper.UserExists(incomeForDeleteDto.Username))
                    return BadRequest("This username doesn't exists");

                var deletedIncomes = await repository.DeleteIncomes(incomeForDeleteDto);

                return StatusCode(204);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}