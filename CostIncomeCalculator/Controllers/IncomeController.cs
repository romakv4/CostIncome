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
    /// <summary>
    /// Income controller. Endpoint for work with income.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeRepository repository;
        private readonly IConfiguration config;
        private readonly IUserHelper userHelper;
        private readonly ITokenHelper tokenHelper;

        /// <summary>
        /// Income controller constructor.
        /// </summary>
        /// <param name="repository">Income repository <see cref="IIncomeRepository" />.</param>
        /// <param name="config">Configuration properties.</param>
        /// <param name="userHelper">User helpers <see cref="IUserHelper" />.</param>
        /// <param name="tokenHelper">JWT tokens helpers <see cref="ITokenHelper" />.</param>
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
        
        /// <summary>
        /// Get all users incomes.
        /// </summary>
        /// <returns>Array of users incomes.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllIncomes()
        {
            try
            {
                string username = HttpContext.User.Identity.Name;

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

        /// <summary>
        ///Get concrete income by id.
        /// </summary>
        /// <param name="id">int</param>
        /// <returns><see cref="IncomeReturnDto" /></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConcreteIncome(int id)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;

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

        /// <summary>
        /// Get all weekly users incomes.
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <returns>Array of weekly users costs.</returns>
        [HttpGet("weekly")]
        public async Task<IActionResult> GetWeeklyIncomes(DateTime date)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;

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

        /// <summary>
        /// Get weekly users incomes by category.
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <param name="category">string</param>
        /// <returns>Array of weekly users incomes in concrete category.</returns>
        [HttpGet("weekly/{category}")]
        public async Task<IActionResult> GetWeeklyIncomesByCategory(DateTime date, string category)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;

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

        /// <summary>
        /// Get all monthly users incomes.
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <returns>Array of all monthly users costs.</returns>
        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyIncomes(DateTime date)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;
                
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

        /// <summary>
        /// Get all monthly users incomes by category.
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <param name="category">string</param>
        /// <returns>Array of monthly users incomes in concrete category.</returns>
        [HttpGet("monthly/{category}")]
        public async Task<IActionResult> GetMonthlyIncomesByCategory(DateTime date, string category)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;

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

        /// <summary>
        /// Get category of users incomes with maximum sum in month.
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <returns>category of incomes with maximum sum.</returns>
        [HttpGet("monthly/max")]
        public async Task<IActionResult> GetMaxMonthlyIncomes(DateTime date)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;

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

        /// <summary>
        /// Set income.
        /// </summary>
        /// <param name="incomeForSetDto"><see cref="IncomeForSetDto" /></param>
        /// <returns>201 if success. 404 if username doesn't exists in database or required fields don't specified.</returns>
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

        /// <summary>
        /// Edit exist cost.
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="incomeForEditDto"><see cref="IncomeForEditDto" /></param>
        /// <returns>204 if success. 404 if username doesn't exists in database or required fields don't specified.</returns>
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditIncome(int id, IncomeForEditDto incomeForEditDto)
        {
            try
            {
                if (!await userHelper.UserExists(incomeForEditDto.Username))
                    return BadRequest("This username doesn't exists");

                if (incomeForEditDto.Category == null &&
                    incomeForEditDto.Description == null &&
                    incomeForEditDto.Price == decimal.MinValue &&
                    incomeForEditDto.Date == DateTime.MinValue)
                    return BadRequest("Required at least one value for edit cost");
                
                var editedIncome = await repository.EditIncome(id, incomeForEditDto);

                if (editedIncome == null) return NotFound();

                return StatusCode(204);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Delete exist cost.
        /// </summary>
        /// <param name="incomeForDeleteDto"><see cref="IncomeForDeleteDto" /></param>
        /// <returns>204 if success. 404 if username doesn't exists in database or required fields don't specified.</returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteIncomes(IncomeForDeleteDto incomeForDeleteDto)
        {
            try
            {
                if (!await userHelper.UserExists(incomeForDeleteDto.Username))
                    return BadRequest("This username doesn't exists");

                if (incomeForDeleteDto.Ids.Length == 0)
                    return BadRequest("Array of ids don't be empty");

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