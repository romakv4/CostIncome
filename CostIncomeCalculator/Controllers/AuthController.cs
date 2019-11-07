using System.Threading.Tasks;
using CostIncomeCalculator.Data.AuthData;
using CostIncomeCalculator.Dtos.UserDtos;
using CostIncomeCalculator.Helpers;
using CostIncomeCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CostIncomeCalculator.Controllers
{
    /// <summary>
    /// Authentication controller. Endpoint for registration and authorization.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repository;
        private readonly IConfiguration config;
        private readonly IUserHelper userHelper;
        private readonly ITokenHelper tokenHelper;

        /// <summary>
        /// Authentication controller constructor.
        /// </summary>
        /// <param name="repository">Authentication repository <see cref="IAuthRepository" />.</param>
        /// <param name="config">Configuration properties.</param>
        /// <param name="userHelper">User helpers <see cref="IUserHelper" />.</param>
        /// <param name="tokenHelper">JWT tokens helpers <see cref="ITokenHelper" /></param>
        public AuthController(IAuthRepository repository, IConfiguration config, IUserHelper userHelper, ITokenHelper tokenHelper)
        {
            this.tokenHelper = tokenHelper;
            this.userHelper = userHelper;
            this.config = config;
            this.repository = repository;
        }

        /// <summary>
        /// User registration endpoint.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/auth/register
        ///     {
        ///         "email": "user email",
        ///         "password": "password"    
        ///     }
        /// </remarks>
        /// <param name="userForRegisterDto">Data for user registration <see cref="UserForRegisterDto" />.</param>
        /// <returns>Registration status</returns>
        /// <response code="201">If user successfully created.</response>
        /// <response code="400">If username already exists in databse.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpPost("register")]
        [Consumes("application/json")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            try
            {
                userForRegisterDto.Email = userForRegisterDto.Email.ToLower();

                if (await userHelper.UserExists(userForRegisterDto.Email))
                    return BadRequest("Email already exists");

                var userToCreate = new User
                {
                    Email = userForRegisterDto.Email
                };

                var createdUser = await repository.Register(userToCreate, userForRegisterDto.Password);

                return StatusCode(201);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// User login endpoint.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/auth/login
        ///     {
        ///         "email": "user email",
        ///         "password": "password"    
        ///     }
        /// </remarks>
        /// <param name="userForLoginDto">Data for user authorization <see cref="UserForLoginDto" />.</param>
        /// <returns>JWT token for user.</returns>
        /// <response code="200">If user successfully login.</response>
        /// <response code="400">If username or password not specified.</response>
        /// <response code="401">If user not exist in database.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpPost("login")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            try
            {
                var user = await repository.Login(userForLoginDto.Email.ToLower(), userForLoginDto.Password);

                if (user == null)
                    return Unauthorized();

                return Ok(new
                {
                    token = tokenHelper.GenerateToken(user, config.GetSection("AppSettings:Token").Value)
                });
            }
            catch
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// User change password endpoint.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/auth/changepassword
        ///     {
        ///         "email": "user email",
        ///         "oldpassword": "old user password",
        ///         "newpassword": "new user password",
        ///     }
        /// </remarks>
        /// <param name="userForChangePasswordDto">Data for user authorization <see cref="UserForChangePasswordDto" />.</param>
        /// <returns>Status code of operation.</returns>
        /// <response code="200">If user successfully changed the password.</response>
        /// <response code="400">If have error in data.</response>
        /// <response code="500">If something went wrong.</response>
        [HttpPost("changepassword")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> ChangePassword(UserForChangePasswordDto userForChangePasswordDto)
        {
            try
            {
                var user = await repository.ChangePassword(
                    userForChangePasswordDto.Email.ToLower(),
                    userForChangePasswordDto.OldPassword,
                    userForChangePasswordDto.NewPassword
                );

                if (user == null)
                    return BadRequest();

                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
