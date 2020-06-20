using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CostIncomeCalculator.Data;
using CostIncomeCalculator.Dtos;
using CostIncomeCalculator.Helpers;
using CostIncomeCalculator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CostIncomeCalculator.Controllers
{
    /// <summary>
    /// Cost controller. Endpoint for work with costs.
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CostController : ControllerBase
    {
        private readonly IAccountingItemRepository costRepository;
        private readonly IConfiguration config;
        private readonly IUserHelper userHelper;
        private readonly ITokenHelper tokenHelper;

        /// <summary>
        /// Cost controller constructor.
        /// </summary>
        /// <param name="costRepository">Accounting item repository <see cref="IAccountingItemRepository" />.</param>
        /// <param name="config">Configuration properties.</param>
        /// <param name="userHelper">User helpers <see cref="IUserHelper" />.</param>
        /// <param name="tokenHelper">JWT tokens helpers <see cref="ITokenHelper" />.</param>
        public CostController(
            IAccountingItemRepository costRepository,
            IConfiguration config,
            IUserHelper userHelper,
            ITokenHelper tokenHelper)
        {
            this.tokenHelper = tokenHelper;
            this.userHelper = userHelper;
            this.config = config;
            this.costRepository = costRepository;
        }

        /// <summary>
        /// Get costs endpoint.
        /// </summary>
        /// <param name="category">May be null or any category of costs defined by user.</param>
        /// <returns><see cref="Cost" /></returns>
        /// <response code="200">Array with all user costs by provided parameters.</response>
        /// <response code="400">If provided parameters are wrong.</response>
        /// <response code="401">If user unathorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Cost>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCosts([FromQuery] string category)
        {
            try
            {
                string email = HttpContext.User.Identity.Name.ToLower();

                if (category == null)
                {
                    var costs = await costRepository.GetAll(email);
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
        /// Get concrete cost by id.
        /// </summary>
        /// <param name="id">int</param>
        /// <returns><see cref="Cost" /></returns>
        /// <response code="200">With concrete cost payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="404">If concrete cost not found in database.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Cost), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetConcreteCost(int id)
        {
            try
            {
                string email = HttpContext.User.Identity.Name.ToLower();

                var concreteCost = await costRepository.GetConcrete(email, id);

                if (concreteCost == null) return NotFound();

                return Ok(concreteCost);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Set cost.
        /// </summary>
        /// <param name="costForSetDto"><see cref="AccountingItemSetDto" /></param>
        /// <returns>Operation status code.</returns>
        /// <response code="201">If successfully created cost.</response>
        /// <response code="400">If provided data for cost is not valid.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetCost(AccountingItemSetDto costForSetDto)
        {
            try
            {   
                var email = HttpContext.User.Identity.Name.ToLower();

                var settedCost = await costRepository.Set(email, costForSetDto);

                return StatusCode(201, new { success = true });
            }
            catch
            {
                return StatusCode(500, new { success = false, message = "Server error. Please, try again later!" } );
            }
        }

        /// <summary>
        /// Edit exist cost.
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="costForEditDto"><see cref="AccountingItemEditDto" /></param>
        /// <returns>Operation status code.</returns>
        /// <response code="204">If successfully edited cost.</response>
        /// <response code="400">If user don't specified at least one field for edit.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="404">If cost for edit not found by specified id.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditCost(int id, AccountingItemEditDto costForEditDto)
        {
            try
            {
                if (costForEditDto.Category == null &&
                    costForEditDto.Description == null &&
                    costForEditDto.Price == decimal.MinValue &&
                    costForEditDto.Date == DateTime.MinValue)
                    return BadRequest("Required at least one for edit cost");

                var email = HttpContext.User.Identity.Name.ToLower();

                var editedCost = await costRepository.Edit(email, id, costForEditDto);

                if (editedCost == null) return NotFound();

                return StatusCode(200, new { success = true });
            }
            catch
            {
                return StatusCode(500, new { success = false, message = "Server error. Please, try again later!" } );
            }
        }

        /// <summary>
        /// Delete exist cost.
        /// </summary>
        /// <param name="costForDeleteDto"><see cref="AccountingItemDeleteDto" /></param>
        /// <returns>Operation status code.</returns>
        /// <response code="204">If successfully deleted cost.</response>
        /// <response code="400">If user don't specified at least one id for delete.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="404">If cost(s) for delete not found by specified id.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCosts(AccountingItemDeleteDto costForDeleteDto)
        {
            try
            {
                var email = HttpContext.User.Identity.Name.ToLower();

                var deletedCosts = await costRepository.Delete(email, costForDeleteDto);

                if (deletedCosts == null) return NotFound();

                return StatusCode(204);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}