using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CostIncomeCalculator.Data.AuthData;
using CostIncomeCalculator.Dtos.UserDtos;
using CostIncomeCalculator.Helpers;
using CostIncomeCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CostIncomeCalculator.Controllers
{
    /// <summary>
    /// Authentication controller. Contains endpoints for registration and authorization.
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
        /// <param name="userForRegisterDto">Data for user registration <see cref="UserForRegisterDto" />.</param>
        /// <returns>404 if username already exists and if login and password dont't specified. If successfully registration returns 201.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            try
            {
                userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

                if (await userHelper.UserExists(userForRegisterDto.Username))
                    return BadRequest("Username already exists");

                var userToCreate = new User
                {
                    Username = userForRegisterDto.Username
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
        /// <param name="userForLoginDto">Data for user authorization <see cref="UserForLoginDto" />.</param>
        /// <returns>Unathorized if user not exists in database. 404 if login or password don't specified. If success returns 200 and JWT access token.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            try
            {
                var user = await repository.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

                if (user == null)
                    return Unauthorized();

                return Ok(new
                {
                    token = tokenHelper.GenerateToken(user, config)
                });
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
