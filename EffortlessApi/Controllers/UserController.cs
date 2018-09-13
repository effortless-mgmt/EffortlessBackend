using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EffortlessApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EffortlessContext _context;

        public UserController(EffortlessContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return _context.Users;
        }

        [HttpGet("{username}")]
        public ActionResult<User> GetByUsername(string username) 
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return NotFound($"User {username} could not be found.");
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);

            if (existingUser != null) 
            {
                return existingUser;
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return await _context.Users.FindAsync(user.Id);
        }
    }
}