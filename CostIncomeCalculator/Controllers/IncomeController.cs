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
    [Route("api/[controller]s")]
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
        /// <response code="200">With users incomes payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
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
        /// <response code="200">With concrete income payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConcreteIncome(int id)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;
                
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
        /// <returns>Array of weekly users incomes.</returns>
        /// <response code="200">With all weekly users incomes payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet("weekly")]
        public async Task<IActionResult> GetWeeklyIncomes(DateTime date)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;

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
        /// <response code="200">With all weekly users incomes in concrete category payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet("weekly/{category}")]
        public async Task<IActionResult> GetWeeklyIncomesByCategory(DateTime date, string category)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;

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
        /// <returns>Array of all monthly users incomes.</returns>
        /// <response code="200">With all monthly users incomes payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyIncomes(DateTime date)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;

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
        /// <response code="200">With all monthly users incomes in cocrete category payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet("monthly/{category}")]
        public async Task<IActionResult> GetMonthlyIncomesByCategory(DateTime date, string category)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;
                
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
        /// <returns>Category of incomes with maximum sum.</returns>
        /// <response code="200">Maximum sum of incomes in cocrete category payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet("monthly/max")]
        public async Task<IActionResult> GetMaxMonthlyIncomes(DateTime date)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;

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
        /// <returns>Operation status code.</returns>
        /// <response code="201">If successfully created income.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpPost("set")]
        public async Task<IActionResult> SetIncome(IncomeForSetDto incomeForSetDto)
        {
            try
            {
                var settedIncome = await repository.SetIncome(incomeForSetDto);
                
                return StatusCode(201);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Edit exist income.
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="incomeForEditDto"><see cref="IncomeForEditDto" /></param>
        /// <returns>Operation status code.</returns>
        /// <response code="204">If successfully edited income.</response>
        /// <response code="400">If user don't specified at least one field for edit.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="404">If income for edit not found by specified id.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditIncome(int id, IncomeForEditDto incomeForEditDto)
        {
            try
            {
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
        /// Delete exist income.
        /// </summary>
        /// <param name="incomeForDeleteDto"><see cref="IncomeForDeleteDto" /></param>
        /// <returns>Operation status code.</returns>
        /// <response code="204">If successfully deleted income.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="404">If income(s) for delete not found by specified id.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteIncomes(IncomeForDeleteDto incomeForDeleteDto)
        {
            try
            {
                var deletedIncomes = await repository.DeleteIncomes(incomeForDeleteDto);

                if (deletedIncomes == null) return NotFound();

                return StatusCode(204);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}