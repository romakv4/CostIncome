using System;
using System.Threading.Tasks;
using CostIncomeCalculator.Data.CostData;
using CostIncomeCalculator.Dtos.CostDtos;
using CostIncomeCalculator.Helpers;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        public async Task<IActionResult> GetAllCosts()
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");

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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConcreteCost(int id)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");

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
        [HttpGet("weekly")]
        public async Task<IActionResult> GetWeeklyCosts(DateTime date)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");

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
        [HttpGet("weekly/{category}")]
        public async Task<IActionResult> GetWeeklyCostsByCategory(DateTime date, string category)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");

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
        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyCosts(DateTime date)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");

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
        [HttpGet("monthly/{category}")]
        public async Task<IActionResult> GetMonthlyCostsByCategory(DateTime date, string category)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");
                
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
        [HttpGet("monthly/max")]
        public async Task<IActionResult> GetMaxMonthlyCosts(DateTime date)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");

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
        /// <returns>201 if success. 404 if username doesn't exists in database or required fields don't specified.</returns>
        [HttpPost("set")]
        public async Task<IActionResult> SetCost(CostForSetDto costForSetDto)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");

                if (costForSetDto.Price == decimal.MinValue ||
                    costForSetDto.Date == DateTime.MinValue)
                    return BadRequest("All fields required");

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
        /// <returns>204 if success. 404 if username doesn't exists in database or required fields don't specified.</returns>
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditCost(int id, CostForEditDto costForEditDto)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");

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
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCosts(CostForDeleteDto costForDeleteDto)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");

                var deletedCosts = await repository.DeleteCosts(costForDeleteDto);

                return StatusCode(204);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}