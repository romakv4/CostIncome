using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace cost_income_calculator.api.Helpers
{
    /// <summary>
    /// TokenHelper class.
    /// </summary>
    public class TokenHelper : ITokenHelper
    {
        /// <summary>
        /// Generate token.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="config">IConfiguration</param>
        /// <returns>Token</returns>
        public string GenerateToken(User user, IConfiguration config)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Get username from HttpContext.
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>Username</returns>
        public string GetUsername(HttpContext context)
        {
            return context.User.Identity.Name;
        }
    }
}