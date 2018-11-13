using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EffortlessApi.Controllers {
    [Route ("api/[controller]")]
    public class AuthController : Controller {
        private readonly EffortlessContext _context;

        public AuthController (EffortlessContext context) {
            _context = context;
        }

        private ClaimsIdentity GetClaimsIdentity (User user) {
            var claims = new List<Claim> {
                new Claim (ClaimTypes.Name, user.UserName),
                new Claim (ClaimTypes.Email, user.Email)
            };

            var claimsIdentity = new ClaimsIdentity (claims, "Token");

            if (user.UserName == "admin") {
                claimsIdentity.AddClaim (new Claim (ClaimTypes.Role, "admin"));
            }

            return claimsIdentity;
        }

        private string GetJwtToken (ClaimsIdentity identity) {
            var secretKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes ("fNGxeQqjhXhRduHA"));
            var signingCredentials = new SigningCredentials (secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken (
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims : identity.Claims,
                expires : DateTime.Now.AddMinutes(5),
                signingCredentials : signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost, Route ("login")]
        public IActionResult Login ([FromBody] User user) {
            if (user == null || user.UserName == null || user.Password == null) {
                return BadRequest ("Invalid client request.");
            }

            // var fetchedUser = await _context.Users.FirstOrDefault(u => u.UserName == user.UserName);
            var fetchedUser = _context.Users.FirstOrDefault (u => u.UserName == user.UserName);

            if (fetchedUser == null || user.Password != fetchedUser.Password) {
                return Forbid ("Username or password is incorrect.");
            }

            var identity = GetClaimsIdentity(fetchedUser);
            var token = GetJwtToken(identity);

            return Ok(new { User = fetchedUser, Token = token });

        }
    }
}