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

        // public UserController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public UserController(EffortlessContext context) => _unitOfWork = new UnitOfWork(context);

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();

            if (users == null) return NotFound();

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(User user)
        {
            await _unitOfWork.Users.AddAsync(user);
            _unitOfWork.Complete();
            return CreatedAtRoute("user/", user);
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> Put(string username, User user)
        {
            // var oldUsers = await _unitOfWork.Users.FindAsync(u => u.Username == username);
            // var oldUser = oldUsers.FirstOrDefault();

            var oldUser = await _unitOfWork.Users.GetByUsernameAsync(username);

            if (oldUser == null) return NotFound($"User {user.Username} could not be found.");

            oldUser.Username = user.Username;
            oldUser.Firstname = user.Firstname;
            oldUser.Lastname = user.Lastname;
            oldUser.Email = user.Email;
            oldUser.Password = user.Password;

            _unitOfWork.Complete();

            return Ok(oldUser);
        }
    }
}