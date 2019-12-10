using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CostIncomeCalculator.Data.LimitData;
using CostIncomeCalculator.Dtos.LimitDtos;
using CostIncomeCalculator.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CostIncomeCalculator.Controllers
{
    /// <summary>
    /// Limit controller. Endpoint for work with limits.
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        /// <response code="200">With users limits payload.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LimitReturnDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllLimits()
        {
            try
            {
                string username = HttpContext.User.Identity.Name;
                
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
        /// <returns>Operation status code.</returns>
        /// <response code="201">If successfully created limit.</response>
        /// <response code="400">If provided data for limit is not valid.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetLimit(LimitForSetDto limitForSetDto)
        {
            try
            {
                string email = HttpContext.User.Identity.Name;

                var settedLimit = await repository.SetLimit(email, limitForSetDto);

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
        /// <returns>Operation status code.</returns>
        /// <response code="204">If successfully edited limit.</response>
        /// <response code="400">If user don't specified at least one field for edit.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="404">If limit for edit not found by specified id.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditLimit(int id, LimitForEditDto limitForEditDto)
        {
            try
            {
                if (limitForEditDto.Category == null &&
                    limitForEditDto.Value == decimal.MinValue &&
                    limitForEditDto.From == DateTime.MinValue &&
                    limitForEditDto.To == DateTime.MinValue)
                    return BadRequest("Required at least one value for edit limit");
                
                string email = HttpContext.User.Identity.Name;

                var editedLimit = await repository.EditLimit(email, id, limitForEditDto);

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
        /// <returns>Operation status code.</returns>
        /// <response code="204">If successfully deleted limit.</response>
        /// <response code="400">If user don't specified at least one id for delete.</response>
        /// <response code="401">If user unauthorized.</response>
        /// <response code="404">If limit(s) for delete not found by specified id.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLimits(LimitForDeleteDto limitForDeleteDto)
        {
            try
            {
                string email = HttpContext.User.Identity.Name;

                var deletedLimits = await repository.DeleteLimits(email, limitForDeleteDto);

                if (deletedLimits == null) return NotFound();

                return StatusCode(204);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}