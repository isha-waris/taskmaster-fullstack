using Microsoft.AspNetCore.Mvc;
using TaskMaster.API.Entities;
using TaskMaster.API.Repositories.Interfaces;

namespace TaskMaster.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _userRepo.GetAllUsersAsync());

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            var newUser = await _userRepo.AddUserAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
        }
    }
}
