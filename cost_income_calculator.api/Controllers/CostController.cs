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

        [HttpPost("setcost")]
        public async Task<IActionResult> SetCost(CostForSetDto costForSetDto)
        {
            if (!await userHelper.UserExists(costForSetDto.Username))
                return BadRequest("This username doesn't exists");

            var settedCost = await repository.SetCost(costForSetDto.Username, costForSetDto.Type, costForSetDto.Description, costForSetDto.Price, costForSetDto.Date);
            
            return StatusCode(201);
        }

        [HttpPut("editcost/{id}")]
        public async Task<IActionResult> EditCost(int id, CostForEditDto costForEditDto)
        {
            if (!await userHelper.UserExists(costForEditDto.Username))
                return BadRequest("This username doesn't exists");
            
            var editedCost = await repository.EditCost(costForEditDto.Username, id, costForEditDto.Type, costForEditDto.Description, costForEditDto.Price, costForEditDto.Date);

            return StatusCode(204);
        }

        [HttpDelete("deletecost/{id}")]
        public async Task<IActionResult> DeleteCost(int id, CostForDeleteDto costForDeleteDto)
        {
            if (!await userHelper.UserExists(costForDeleteDto.Username))
                return BadRequest("This username doesn't exists");

            var deletedCost = await repository.DeleteCost(costForDeleteDto.Username, id);

            return StatusCode(204);
        }
    }
}