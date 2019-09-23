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
    /// <summary>
    /// Limit controller. Endpoint for work with limits.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LimitController : ControllerBase
    {
        private readonly ILimitRepository repository;
        private readonly IConfiguration config;
        private readonly IUserHelper userHelper;
        private readonly ITokenHelper tokenHelper;

        /// <summary>
        /// Limit controller constructor.
        /// </summary>
        /// <param name="repository">Limit repository <see cref="ILimitRepository" />.</param>
        /// <param name="config">Configuration properties.</param>
        /// <param name="userHelper">User helpers <see cref="IUserHelper" />.</param>
        /// <param name="tokenHelper">JWT token helpers <see cref="ITokenHelper" />.</param>
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

        /// <summary>
        /// Get all users limits.
        /// </summary>
        /// <returns>Array of users limits.</returns>
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

        /// <summary>
        /// Set limit.
        /// </summary>
        /// <param name="limitForSetDto"><see cref="LimitForSetDto" /></param>
        /// <returns>201 if success. 404 if username doesn't exists in database or required fields don't specified.</returns>
        [HttpPost("set")]
        public async Task<IActionResult> SetLimit(LimitForSetDto limitForSetDto)
        {
            try
            {
                string username = tokenHelper.GetUsername(HttpContext);

                if (!await userHelper.UserExists(username))
                    return BadRequest("This username doesn't exists");

                var settedCost = await repository.SetLimit(limitForSetDto);

                return StatusCode(201);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Edit exist limit.
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="limitForEditDto"><see cref="LimitForEditDto" />.</param>
        /// <returns>204 if success. 404 if username doesn't exists in database or required fields don't specified.</returns>
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

        /// <summary>
        /// Delete exist limit.
        /// </summary>
        /// <param name="limitForDeleteDto"><see cref="LimitForDeleteDto" /></param>
        /// <returns>204 if success. 404 if username doesn't exists in database or required fields don't specified.</returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteLimits(LimitForDeleteDto limitForDeleteDto)
        {
            try
            {
                if (!await userHelper.UserExists(limitForDeleteDto.Username))
                    return BadRequest("This username doesn't exists");

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