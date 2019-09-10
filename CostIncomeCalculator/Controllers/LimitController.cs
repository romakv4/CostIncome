using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CostIncomeCalculator.Data.LimitData;
using CostIncomeCalculator.Dtos.LimitDtos;
using CostIncomeCalculator.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CostIncomeCalculator.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LimitController : ControllerBase
    {
        private readonly ILimitRepository repository;
        private readonly IConfiguration config;
        private readonly IUserHelper userHelper;
        private readonly ITokenHelper tokenHelper;

        public LimitController(
            ILimitRepository repository,
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
        public async Task<IActionResult> GetAllLimits()
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");
                
                var limits = await repository.GetAllLimits(username);
                
                return Ok(limits);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("set")]
        public async Task<IActionResult> SetLimit(LimitForSetDto limitForSetDto)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");

                if (limitForSetDto.Value == decimal.MinValue ||
                    limitForSetDto.From == DateTime.MinValue ||
                    limitForSetDto.To == DateTime.MinValue)
                    return BadRequest("All fields required");

                if (limitForSetDto.From == limitForSetDto.To)
                    return BadRequest("Limit start date don't be equal limit stop date");
                
                if (limitForSetDto.From >= limitForSetDto.To)
                    return BadRequest("Limit start date must be earlier limit stop date");

                var settedCost = await repository.SetLimit(limitForSetDto);

                return StatusCode(201);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditLimit(int id, LimitForEditDto limitForEditDto)
        {
            try
            {
                if (!await userHelper.UserExists(limitForEditDto.Username))
                    return BadRequest("This username doesn't exists");

                if (limitForEditDto.Category == null &&
                    limitForEditDto.Value == decimal.MinValue &&
                    limitForEditDto.From == DateTime.MinValue &&
                    limitForEditDto.To == DateTime.MinValue)
                    return BadRequest("Required at least one value for edit limit");
                
                var editedLimit = await repository.EditLimit(id, limitForEditDto);

                if (editedLimit == null) return NotFound();

                return StatusCode(204);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteLimits(LimitForDeleteDto limitForDeleteDto)
        {
            try
            {
                if (!await userHelper.UserExists(limitForDeleteDto.Username))
                    return BadRequest("This username doesn't exists");

                if (limitForDeleteDto.Ids.Length == 0)
                    return BadRequest("Array of ids don't be empty");

                var deletedIncomes = await repository.DeleteLimits(limitForDeleteDto);

                return StatusCode(204);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}