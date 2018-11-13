using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EffortlessApi.Core;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(EffortlessContext context) => _unitOfWork = new UnitOfWork(context);

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();

            if (users == null) return NotFound();

            return Ok(users);
        }

        [HttpGet("{username}", Name = "GetUser")]
        public async Task<IActionResult> GetByUsername(string userName) 
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(userName);

            if (user == null)
            {
                return NotFound($"User {userName} could not be found.");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(User user)
        {
            var existingUser = await _unitOfWork.Users.GetByUsernameAsync(user.UserName);

            if (existingUser != null) return Ok(user);

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return CreatedAtRoute("GetUser", new { userName = user.UserName}, user);
        }

        [HttpPut("{userName}")]
        public async Task<IActionResult> Put(string userName, User user)
        {
            var existing = await _unitOfWork.Users.GetByUsernameAsync(userName);

            if (existing == null) return NotFound($"User {user.UserName} could not be found.");

            await _unitOfWork.Users.UpdateAsync(userName, user);
            await _unitOfWork.CompleteAsync();

            return Ok(user);
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> Delete(string userName)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(userName);

            if (user == null)
            {
                return NotFound($"User {userName} could not be found.");
            }
            
            _unitOfWork.Users.Remove(user);

            await _unitOfWork.CompleteAsync();

            return Ok(user);
        }
    }
}