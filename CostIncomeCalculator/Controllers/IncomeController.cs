using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CostIncomeCalculator.Data;
using CostIncomeCalculator.Data.IncomeData;
using CostIncomeCalculator.Dtos;
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
        private readonly IIncomeRepository incomeRepository;
        private readonly IConfiguration config;
        private readonly IUserHelper userHelper;
        private readonly ITokenHelper tokenHelper;

        /// <summary>
        /// Income controller constructor.
        /// </summary>
        /// <param name="incomeRepository">Income repository <see cref="IIncomeRepository" />.</param>
        /// <param name="config">Configuration properties.</param>
        /// <param name="userHelper">User helpers <see cref="IUserHelper" />.</param>
        /// <param name="tokenHelper">JWT tokens helpers <see cref="ITokenHelper" />.</param>
        public IncomeController(
            IIncomeRepository incomeRepository,
            IConfiguration config,
            IUserHelper userHelper,
            ITokenHelper tokenHelper)
        {
            this.userHelper = userHelper;
            this.tokenHelper = tokenHelper;
            this.config = config;
            this.incomeRepository = incomeRepository;
        }

        /// <summary>
        /// Get incomes endpoint.
        /// </summary>
        /// <param name="category">May be null or any category of incomes defined by user.</param>
        /// <returns><see cref="AccountingItem" /></returns>
        /// <response code="200">Array with all user incomes by provided parameters.</response>
        /// <response code="400">If provided parameters are wrong.</response>
        /// <response code="401">If user unathorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<AccountingItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetIncomes([FromQuery] string category)
        {
            try
            {
                string email = HttpContext.User.Identity.Name.ToLower();

                if (category == null)
                {
                    var costs = await incomeRepository.GetAll(email);
                    return Ok(costs);
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
        /// <returns><see cref="AccountingItem" /></returns>
        /// <response code="200">With concrete income payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="404">If concrete income not found in database.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AccountingItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetConcreteIncome(int id)
        {
            try
            {
                string email = HttpContext.User.Identity.Name.ToLower();
                
                var concreteIncome = await incomeRepository.GetConcrete(email, id);
                
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
        /// <param name="incomeForSetDto"><see cref="AccountingItemSetDto" /></param>
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
        public async Task<IActionResult> SetIncome(AccountingItemSetDto incomeForSetDto)
        {
            try
            {
                string email = HttpContext.User.Identity.Name.ToLower();

                var settedIncome = await incomeRepository.Set(email, incomeForSetDto);
                
                return StatusCode(201, new { success = true });
            }
            catch
            {
                return StatusCode(500, new { success = false, message = "Server error. Please, try again later!" });
            }
        }

        /// <summary>
        /// Edit exist income.
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="incomeForEditDto"><see cref="AccountingItemEditDto" /></param>
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
        public async Task<IActionResult> EditIncome(int id, AccountingItemEditDto incomeForEditDto)
        {
            try
            {
                if (incomeForEditDto.Category == null &&
                    incomeForEditDto.Description == null &&
                    incomeForEditDto.Price == decimal.MinValue &&
                    incomeForEditDto.Date == DateTime.MinValue)
                    return BadRequest("Required at least one value for edit cost");

                string email = HttpContext.User.Identity.Name.ToLower();
                
                var editedIncome = await incomeRepository.Edit(email, id, incomeForEditDto);

                if (editedIncome == null) return NotFound();

                return StatusCode(200, new { success = true });
            }
            catch
            {
                return StatusCode(500, new { success = false, message = "Server error. Please, try again later!" });
            }
        }

        /// <summary>
        /// Delete exist income.
        /// </summary>
        /// <param name="incomeForDeleteDto"><see cref="AccountingItemDeleteDto" /></param>
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
        public async Task<IActionResult> DeleteIncomes(AccountingItemDeleteDto incomeForDeleteDto)
        {
            try
            {
                string email = HttpContext.User.Identity.Name.ToLower();

                var deletedIncomes = await incomeRepository.Delete(email, incomeForDeleteDto);

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