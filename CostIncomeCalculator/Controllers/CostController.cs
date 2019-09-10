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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CostController : ControllerBase
    {
        private readonly ICostRepository repository;
        private readonly IConfiguration config;
        private readonly IUserHelper userHelper;
        private readonly ITokenHelper tokenHelper;

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

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCosts(CostForDeleteDto costForDeleteDto)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");

                if (costForDeleteDto.Ids.Length == 0)
                    return BadRequest("Array of ids don't be empty");

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