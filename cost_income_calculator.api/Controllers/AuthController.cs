using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using cost_income_calculator.api.Data.AuthData;
using cost_income_calculator.api.Dtos.UserDtos;
using cost_income_calculator.api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace cost_income_calculator.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repository;
        private readonly IConfiguration config;
        private readonly IUserHelper userHelper;
        private readonly ITokenHelper tokenHelper;

        public AuthController(IAuthRepository repository, IConfiguration config, IUserHelper userHelper, ITokenHelper tokenHelper)
        {
            this.tokenHelper = tokenHelper;
            this.userHelper = userHelper;
            this.config = config;
            this.repository = repository;
        }

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
