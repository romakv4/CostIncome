using System.Threading.Tasks;
using cost_income_calculator.api.Data.IncomeData;
using cost_income_calculator.api.Dtos.IncomeDtos;
using cost_income_calculator.api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace cost_income_calculator.api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeRepository repository;
        private readonly IConfiguration config;
        private readonly IUserHelper userHelper;
        public IncomeController(IIncomeRepository repository, IConfiguration config, IUserHelper userHelper)
        {
            this.userHelper = userHelper;
            this.config = config;
            this.repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllIncomes(IncomeForGetDto incomeForGetDto)
        {
            if (!await userHelper.UserExists(incomeForGetDto.Username))
                return BadRequest("This username doesn't exists");
            
            var incomes = await repository.GetAllIncomes(incomeForGetDto.Username);
            
            return Ok(incomes);
        }

        [HttpPost("set")]
        public async Task<IActionResult> SetIncome(IncomeForSetDto incomeForSetDto)
        {
            if (!await userHelper.UserExists(incomeForSetDto.Username))
                return BadRequest("This username doesn't exists");

            var settedIncome = await repository.SetIncome(incomeForSetDto.Username, incomeForSetDto.Type, incomeForSetDto.Description, incomeForSetDto.Price, incomeForSetDto.Date);
            
            return StatusCode(201);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditIncome(int id, IncomeForEditDto incomeForEditDto)
        {
            if (!await userHelper.UserExists(incomeForEditDto.Username))
                return BadRequest("This username doesn't exists");
            
            var editedIncome = await repository.EditIncome(incomeForEditDto.Username, id, incomeForEditDto.Type, incomeForEditDto.Description, incomeForEditDto.Price, incomeForEditDto.Date);

            return StatusCode(204);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteIncomes(ManyIncomesForDeleteDto manyIncomesForDeleteDto)
        {
            if (!await userHelper.UserExists(manyIncomesForDeleteDto.Username))
                return BadRequest("This username doesn't exists");

            var deletedIncomes = await repository.DeleteIncomes(manyIncomesForDeleteDto.Username, manyIncomesForDeleteDto.Ids);

            return StatusCode(204);
        }
    }
}