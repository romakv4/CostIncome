using System;
using System.Threading.Tasks;
using CostIncomeCalculator.Data.CostData;
using CostIncomeCalculator.Dtos.CostDtos;
using CostIncomeCalculator.Helpers;
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
    [Route("api/[controller]")]
    [ApiController]
    public class CostController : ControllerBase
    {
        private readonly ICostRepository repository;
        private readonly IConfiguration config;
        private readonly IUserHelper userHelper;
        private readonly ITokenHelper tokenHelper;

        /// <summary>
        /// Cost controller constructor.
        /// </summary>
        /// <param name="repository">Cost repository <see cref="ICostRepository" />.</param>
        /// <param name="config">Configuration properties.</param>
        /// <param name="userHelper">User helpers <see cref="IUserHelper" />.</param>
        /// <param name="tokenHelper">JWT tokens helpers <see cref="ITokenHelper" />.</param>
        public CostController(
            ICostRepository repository,
            IConfiguration config,
            IUserHelper userHelper,
            ITokenHelper tokenHelper)
        {
            this.tokenHelper = tokenHelper;
            this.userHelper = userHelper;
            this.config = config;
            this.repository = repository;
        }

        /// <summary>
        /// Get all users costs.
        /// </summary>
        /// <returns>Array of users costs.</returns>
        /// <response code="200">With users costs payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllCosts()
        {
            try
            {
                string username = HttpContext.User.Identity.Name;

                var costs = await repository.GetAllCosts(username);
                
                return Ok(costs);
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
        /// <returns><see cref="CostReturnDto" /></returns>
        /// <response code="200">With concrete cost payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConcreteCost(int id)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;

                var concreteCost = await repository.GetConcreteCost(username, id);

                if (concreteCost == null) return NotFound();

                return Ok(concreteCost);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get all weekly users costs.
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <returns>Array of weekly users costs.</returns>
        /// <response code="200">With all weekly users costs payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet("weekly")]
        public async Task<IActionResult> GetWeeklyCosts(DateTime date)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;

                var DTO = new PeriodicCostsDto {
                    Username = username,
                    Date = date
                };

                var weeklyCosts = await repository.GetWeeklyCosts(DTO);

                return Ok(weeklyCosts);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get weekly users costs by category.
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <param name="category">string</param>
        /// <returns>Array of weekly users costs in concrete category.</returns>
        /// <response code="200">With all weekly users costs in concrete category payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet("weekly/{category}")]
        public async Task<IActionResult> GetWeeklyCostsByCategory(DateTime date, string category)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;

                var DTO = new PeriodicCostsDto {
                    Username = username,
                    Date = date
                };

                var weeklyCostsByCategory = await repository.GetWeeklyCostsByCategory(DTO, category);

                return Ok(weeklyCostsByCategory);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get all monthly users costs.
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <returns>Array of all monthly users costs.</returns>
        /// <response code="200">With all monthly users costs payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyCosts(DateTime date)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;

                var DTO = new PeriodicCostsDto {
                    Username = username,
                    Date = date
                };

                var monthlyCosts = await repository.GetMonthlyCosts(DTO);

                return Ok(monthlyCosts);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get all monthly users costs by category.
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <param name="category">string</param>
        /// <returns>Array of monthly users costs in concrete category.</returns>
        /// <response code="200">With all monthly users costs in cocrete category payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet("monthly/{category}")]
        public async Task<IActionResult> GetMonthlyCostsByCategory(DateTime date, string category)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;
                
                var DTO = new PeriodicCostsDto {
                    Username = username,
                    Date = date
                };

                var monthlyCostsByCategory = await repository.GetMonthlyCostsByCategory(DTO, category);

                return Ok(monthlyCostsByCategory);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get category of users costs with maximum sum in month.
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <returns>Category of costs with maximum sum.</returns>
        /// <response code="200">Maximum sum of costs in cocrete category payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet("monthly/max")]
        public async Task<IActionResult> GetMaxMonthlyCosts(DateTime date)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;

                var DTO = new PeriodicCostsDto {
                    Username = username,
                    Date = date
                };

                var maxMonthlyCosts = await repository.GetMaxCostsCategoryInMonth(DTO);

                return Ok(maxMonthlyCosts);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Set cost.
        /// </summary>
        /// <param name="costForSetDto"><see cref="CostForSetDto" /></param>
        /// <returns>Operation status code.</returns>
        /// <response code="201">If successfully created cost.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpPost("set")]
        public async Task<IActionResult> SetCost(CostForSetDto costForSetDto)
        {
            try
            {
                var settedCost = await repository.SetCost(costForSetDto);

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
        /// <param name="costForEditDto"><see cref="CostForEditDto" /></param>
        /// <returns>Operation status code.</returns>
        /// <response code="204">If successfully edited cost.</response>
        /// <response code="400">If user don't specified at least one field for edit.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="404">If cost for edit not found by specified id.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditCost(int id, CostForEditDto costForEditDto)
        {
            try
            {
                if (costForEditDto.Category == null &&
                    costForEditDto.Description == null &&
                    costForEditDto.Price == decimal.MinValue &&
                    costForEditDto.Date == DateTime.MinValue)
                    return BadRequest("Required at least one for edit cost");

                var editedCost = await repository.EditCost(id, costForEditDto);

                if (editedCost == null) return NotFound();

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
        /// <param name="costForDeleteDto"><see cref="CostForDeleteDto" /></param>
        /// <returns>204 if success. 404 if username doesn't exists in database or required fields don't specified.</returns>
        /// <response code="204">If successfully deleted cost.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="404">If cost(s) for delete not found by specified id.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCosts(CostForDeleteDto costForDeleteDto)
        {
            try
            {
                var deletedCosts = await repository.DeleteCosts(costForDeleteDto);

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