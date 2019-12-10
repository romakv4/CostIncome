using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CostIncomeCalculator.Data.IncomeData;
using CostIncomeCalculator.Dtos.IncomeDtos;
using CostIncomeCalculator.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CostIncomeCalculator.Controllers
{
    /// <summary>
    /// Income controller. Endpoint for work with income.
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        /// Get incomes endpoint.
        /// </summary>
        /// <param name="period">May be null, weekly or monthly.</param>
        /// <param name="category">May be null or any category of incomes defined by user.</param>
        /// <param name="date">Date for periodic request.</param>
        /// <returns><see cref="IncomeReturnDto" /></returns>
        /// <response code="200">Array with all user incomes by provided parameters.</response>
        /// <response code="400">If provided parameters are wrong.</response>
        /// <response code="401">If user unathorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IncomeReturnDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetIncomes([FromQuery] string period, string category, DateTime date)
        {
            try
            {
                string email = HttpContext.User.Identity.Name;

                if (period == null && category == null)
                {
                    var costs = await repository.GetAllIncomes(email);
                    return Ok(costs);
                }
                else if (period != null)
                {
                    var DTO = new PeriodicIncomesDto {
                        Email = email,
                        Date = date
                    };
                    if (category == null)
                    {   
                        if (period == "weekly") {
                            var costs = await repository.GetWeeklyIncomes(DTO);
                            return Ok(costs);
                        }
                        else if (period == "monthly")
                        {
                            var costs = await repository.GetMonthlyIncomes(DTO);
                            return Ok(costs);
                        }
                        return BadRequest();
                    }
                    else
                    {
                        if (period == "weekly") {
                            var costs = await repository.GetWeeklyIncomesByCategory(DTO, category);
                            return Ok(costs);
                        }
                        else if (period == "monthly")
                        {
                            var costs = await repository.GetMonthlyIncomesByCategory(DTO, category);
                            return Ok(costs);
                        }
                        return BadRequest();
                    }
                }
                else 
                {
                    return BadRequest();
                }
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
        /// <response code="404">If concrete income not found in database.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IncomeReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetConcreteIncome(int id)
        {
            try
            {
                string email = HttpContext.User.Identity.Name;
                
                var concreteIncome = await repository.GetConcreteIncome(email, id);
                
                if (concreteIncome == null) return NotFound();
                
                return Ok(concreteIncome);
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
        /// <response code="400">If provided data for income is not valid.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// <response code="400">If user don't specified at least one id for delete.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="404">If income(s) for delete not found by specified id.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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