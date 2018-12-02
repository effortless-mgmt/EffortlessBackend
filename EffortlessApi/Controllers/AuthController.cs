using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EffortlessApi.Core;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EffortlessApi.Controllers 
{
    [Route("api/[controller]")]
    public class AuthController : Controller 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtSettings _jwtSettings;

        public AuthController(EffortlessContext context, IJwtSettings jwtSettings) 
        {
            _unitOfWork = new UnitOfWork(context);
            _jwtSettings = jwtSettings;
        }

        private async Task<ClaimsIdentity> GetClaimsIdentityAsync(User user) 
        {
            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token");

            var fetchedUser = await _unitOfWork.Users.GetByUsernameAsync(user.UserName);

            foreach (var userRole in fetchedUser.UserRoles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }

            return claimsIdentity;
        }

        private string GetJwtToken(ClaimsIdentity identity) 
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                // audience: "http://localhost:5000",
                claims : identity.Claims,
                expires : DateTime.Now.AddMinutes(5),
                signingCredentials : signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] User user) 
        {
            if (user == null || user.UserName == null || user.Password == null) 
            {
                return BadRequest("Invalid client request.");
            }

            var fetchedUser = await _unitOfWork.Users.GetByUsernameAsync(user.UserName);

            if (fetchedUser == null || user.Password != fetchedUser.Password) 
            {
                return Forbid("Username or password is incorrect.");
            }

            var identity = await GetClaimsIdentityAsync(fetchedUser);
            var token = GetJwtToken(identity);

            return Ok(new { User = fetchedUser, Token = token });
        }

        [HttpGet("config"), Authorize(Roles = "admin")]
        public IActionResult GetSettings()
        {
            return Ok($"Signing Key: {_jwtSettings.SigningKey}, Issuer: {_jwtSettings.Issuer}");
        }
    }
}