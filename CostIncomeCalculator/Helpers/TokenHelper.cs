using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CostIncomeCalculator.Models;
using Microsoft.IdentityModel.Tokens;

namespace CostIncomeCalculator.Helpers
{
    /// <summary>
    /// TokenHelper class.
    /// Contains methods to simply work with JWT tokens.
    /// </summary>
    public class TokenHelper : ITokenHelper
    {
        /// <summary>
        /// Generate token.
        /// </summary>
        /// <param name="user"><see cref="User" /></param>
        /// <param name="secret">string</param>
        /// <returns>Token</returns>
        public string GenerateToken(User user, string secret)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

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
    }
}