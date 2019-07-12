using System.Threading.Tasks;
using cost_income_calculator.api.Data.CostData;
using cost_income_calculator.api.Dtos.CostDtos;
using cost_income_calculator.api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace cost_income_calculator.api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CostController : ControllerBase
    {
        private readonly ICostRepository repository;
        private readonly IConfiguration config;
        private readonly IUserHelper userHelper;

        public CostController(ICostRepository repository, IConfiguration config, IUserHelper userHelper)
        {
            this.userHelper = userHelper;
            this.config = config;
            this.repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllCosts(CostForGetDto costForGetDto)
        {
            try
            {
                if (!await userHelper.UserExists(costForGetDto.Username))
                    return BadRequest("This username doesn't exists");
            
                var costs = await repository.GetAllCosts(costForGetDto);
                return Ok(costs);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("weekly")]
        public async Task<IActionResult> GetWeeklyCosts(PeriodicCostsDto periodicCostsDto)
        {
            try
            {
                if (!await userHelper.UserExists(periodicCostsDto.Username))
                    return BadRequest("This username doesn't exists");
                
                var weeklyCosts = await repository.GetWeeklyCosts(periodicCostsDto);
                
                return Ok(weeklyCosts);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("weekly/{category}")]
        public async Task<IActionResult> GetWeeklyCostsByCategory(PeriodicCostsDto periodicCostsDto, string category)
        {
            try
            {
                if (!await userHelper.UserExists(periodicCostsDto.Username))
                    return BadRequest("This username doesn't exists");
            
                var weeklyCostsByCategory = await repository.GetWeeklyCostsByCategory(periodicCostsDto, category);
                
                return Ok(weeklyCostsByCategory);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("monthly")]
        public async Task<IActionResult> GetMonthlyCosts(PeriodicCostsDto periodicCostsDto)
        {
            try
            {
                if (!await userHelper.UserExists(periodicCostsDto.Username))
                    return BadRequest("This username doesn't exists");
            
                var monthlyCosts = await repository.GetMonthlyCosts(periodicCostsDto);
                
                return Ok(monthlyCosts);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("monthly/{category}")]
        public async Task<IActionResult> GetMonthlyCostsByCategory(PeriodicCostsDto periodicCostsDto, string category)
        {
            try
            {
                if (!await userHelper.UserExists(periodicCostsDto.Username))
                    return BadRequest("This username doesn't exists");
            
                var monthlyCostsByCategory = await repository.GetMonthlyCostsByCategory(periodicCostsDto, category);
                
                return Ok(monthlyCostsByCategory);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("monthly/max")]
        public async Task<IActionResult> GetMaxMonthlyCosts(PeriodicCostsDto periodicCostsDto)
        {
            try
            {
                if (!await userHelper.UserExists(periodicCostsDto.Username))
                    return BadRequest("This username doesn't exists");
            
                var maxMonthlyCosts = await repository.GetMaxCostsCategoryInMonth(periodicCostsDto);
                
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
                if (!await userHelper.UserExists(costForSetDto.Username))
                    return BadRequest("This username doesn't exists");

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
                if (!await userHelper.UserExists(costForEditDto.Username))
                    return BadRequest("This username doesn't exists");
            
                var editedCost = await repository.EditCost(id, costForEditDto);

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
                if (!await userHelper.UserExists(costForDeleteDto.Username))
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