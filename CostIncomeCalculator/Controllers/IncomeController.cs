using System;
using System.Threading.Tasks;
using CostIncomeCalculator.Data.IncomeData;
using CostIncomeCalculator.Dtos.IncomeDtos;
using CostIncomeCalculator.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CostIncomeCalculator.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeRepository repository;
        private readonly IConfiguration config;
        private readonly IUserHelper userHelper;
        private readonly ITokenHelper tokenHelper;

        public IncomeController(
            IIncomeRepository repository,
            IConfiguration config,
            IUserHelper userHelper,
            ITokenHelper tokenHelper)
        {
            this.userHelper = userHelper;
            this.tokenHelper = tokenHelper;
            this.config = config;
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIncomes()
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");
                
                var incomes = await repository.GetAllIncomes(username);
                
                return Ok(incomes);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConcreteIncome(int id)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");
                
                var concreteIncome = await repository.GetConcreteIncome(username, id);
                
                if (concreteIncome == null) return NotFound();
                
                return Ok(concreteIncome);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("weekly")]
        public async Task<IActionResult> GetWeeklyIncomes(DateTime date)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");

                var DTO = new PeriodicIncomesDto {
                    Username = username,
                    Date = date
                };
                
                var weeklyIncomes = await repository.GetWeeklyIncomes(DTO);
                
                return Ok(weeklyIncomes);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("weekly/{category}")]
        public async Task<IActionResult> GetWeeklyIncomesByCategory(DateTime date, string category)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");

                var DTO = new PeriodicIncomesDto {
                    Username = username,
                    Date = date
                };
                
                var weeklyIncomesByCategory = await repository.GetWeeklyIncomesByCategory(DTO, category);
                
                return Ok(weeklyIncomesByCategory);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyIncomes(DateTime date)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);
                
                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");

                var DTO = new PeriodicIncomesDto {
                    Username = username,
                    Date = date
                };
                
                var monthlyIncomes = await repository.GetMonthlyIncomes(DTO);
                
                return Ok(monthlyIncomes);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("monthly/{category}")]
        public async Task<IActionResult> GetMonthlyIncomesByCategory(DateTime date, string category)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");
                
                var DTO = new PeriodicIncomesDto {
                    Username = username,
                    Date = date
                };
                
                var monthlyIncomesByCategory = await repository.GetMonthlyIncomesByCategory(DTO, category);
                
                return Ok(monthlyIncomesByCategory);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("monthly/max")]
        public async Task<IActionResult> GetMaxMonthlyIncomes(DateTime date)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");

                var DTO = new PeriodicIncomesDto {
                    Username = username,
                    Date = date
                };
                
                var maxMonthlyIncomes = await repository.GetMaxIncomesCategoryInMonth(DTO);
                
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

                if (editedIncome == null) return NotFound();

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