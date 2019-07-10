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
            if (!await userHelper.UserExists(costForGetDto.Username))
                return BadRequest("This username doesn't exists");
            
            var costs = await repository.GetAllCosts(costForGetDto.Username);
            
            return Ok(costs);
        }

        [HttpPost("weekly")]
        public async Task<IActionResult> GetWeeklyCosts(PeriodicCostsDto periodicCostsDto)
        {
            if (!await userHelper.UserExists(periodicCostsDto.Username))
                return BadRequest("This username doesn't exists");
            
            var weeklyCosts = await repository.GetWeeklyCosts(periodicCostsDto.Username, periodicCostsDto.Date);
            
            return Ok(weeklyCosts);
        }

        [HttpPost("monthly")]
        public async Task<IActionResult> GetMonthlyCosts(PeriodicCostsDto periodicCostsDto)
        {
            if (!await userHelper.UserExists(periodicCostsDto.Username))
                return BadRequest("This username doesn't exists");
            
            var monthlyCosts = await repository.GetMonthlyCosts(periodicCostsDto.Username, periodicCostsDto.Date);
            
            return Ok(monthlyCosts);
        }

        [HttpPost("monthly/max")]
        public async Task<IActionResult> GetMaxMonthlyCosts(PeriodicCostsDto periodicCostsDto)
        {
            if (!await userHelper.UserExists(periodicCostsDto.Username))
                return BadRequest("This username doesn't exists");
            
            var maxMonthlyCosts = await repository.GetMaxCostsCategoryInMonth(periodicCostsDto.Username, periodicCostsDto.Date);
            
            return Ok(maxMonthlyCosts);
        }

        [HttpPost("set")]
        public async Task<IActionResult> SetCost(CostForSetDto costForSetDto)
        {
            if (!await userHelper.UserExists(costForSetDto.Username))
                return BadRequest("This username doesn't exists");

            var settedCost = await repository.SetCost(costForSetDto.Username, costForSetDto.Type, costForSetDto.Description, costForSetDto.Price, costForSetDto.Date);
            
            return StatusCode(201);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditCost(int id, CostForEditDto costForEditDto)
        {
            if (!await userHelper.UserExists(costForEditDto.Username))
                return BadRequest("This username doesn't exists");
            
            var editedCost = await repository.EditCost(costForEditDto.Username, id, costForEditDto.Type, costForEditDto.Description, costForEditDto.Price, costForEditDto.Date);

            return StatusCode(204);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCosts(ManyCostsForDeleteDto manyCostForDeleteDto)
        {
            if (!await userHelper.UserExists(manyCostForDeleteDto.Username))
                return BadRequest("This username doesn't exists");

            var deletedCosts = await repository.DeleteCosts(manyCostForDeleteDto.Username, manyCostForDeleteDto.Ids);

            return StatusCode(204);
        }
    }
}