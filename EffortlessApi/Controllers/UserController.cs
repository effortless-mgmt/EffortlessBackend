using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EffortlessApi.Core;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(EffortlessContext context) => _unitOfWork = new UnitOfWork(context);

        [HttpGet, Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();

            if (users == null) return NotFound();

            return Ok(users);
        }

        [HttpGet("{username}"), Authorize]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByUsernameAsync(string userName)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(userName);

            if (user == null)
            {
                return NotFound($"User {userName} could not be found.");
            }

            return Ok(user);
        }

        [HttpPost, Authorize]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(201, Type = typeof(User))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostAsync(User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingUser = await _unitOfWork.Users.GetByUsernameAsync(user.UserName);

            if (existingUser != null) return Ok(existingUser);

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetByUsernameAsync), new { userName = user.UserName}, user);
        }

        [HttpPut("{userName}"), Authorize]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(string userName, User user)
        {
            var existing = await _unitOfWork.Users.GetByUsernameAsync(userName);

            if (existing == null) return NotFound($"User {user.UserName} could not be found.");

            await _unitOfWork.Users.UpdateAsync(userName, user);
            await _unitOfWork.CompleteAsync();

            return Ok(existing);
        }

        [HttpDelete("{userName}"), Authorize]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(203)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string userName)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(userName);

            if (user == null)
            {
                return NoContent();
            }
            
            _unitOfWork.Users.Remove(user);

            await _unitOfWork.CompleteAsync();

            return Ok(user);
        }
    }
}