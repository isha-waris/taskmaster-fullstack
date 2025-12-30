using Microsoft.AspNetCore.Mvc;
using TaskMaster.API.DTOs.User;
using TaskMaster.API.DTOs.Common;
using TaskMaster.API.Entities;
using TaskMaster.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace TaskMaster.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        //Constructor
        public UsersController(IUserRepository userRepo) => _userRepo = userRepo;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.GetAllUsersAsync();
            var result = users.Select(u => new UserListDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Role = (int)u.Role
            });
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            var response = new UserWithTasksResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = (int)user.Role,

                AssignedTasks = user.AssignedTasks.Select(t => new UserTaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status.ToString(),

                    StatusHistory = t.StatusHistory.Select(h => new TaskStatusHistoryDto
                    {
                        OldStatus = h.OldStatus.ToString(),
                        NewStatus = h.NewStatus.ToString(),
                        ChangedAt = h.ChangedAt,
                        ChangedByUserId = h.ChangedByUserId
                    }).ToList()

                }).ToList()
            };

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                Role = dto.Role,
                CreatedAt = DateTime.UtcNow
            };
            await _userRepo.AddUserAsync(user);
            var responseDto = new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = (int)user.Role,
                CreatedAt = user.CreatedAt
            };
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, responseDto);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponseDto>> Update(int id, [FromBody] UpdateUserDto dto)
        {
            var existing = await _userRepo.GetUserByIdAsync(id);
            if (existing is null) return NotFound();
            existing.FullName = dto.FullName;
            existing.Email = dto.Email;
            existing.Role = dto.Role;
            var updated = await _userRepo.UpdateUserAsync(existing);
            if (updated is null) return NotFound();

            var result = new UserResponseDto
            {
                Id = updated.Id,
                FullName = updated.FullName,
                Email = updated.Email,
                Role = (int)updated.Role,
                CreatedAt = updated.CreatedAt
            };
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _userRepo.DeleteUserAsync(id);
            return ok ? NoContent() : NotFound();
        }

        [HttpPost("{id:int}/change-password")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordDto dto)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            // Simple password check (plain text)
            if (user.PasswordHash != dto.CurrentPassword)
                return BadRequest("Current password is incorrect.");

            user.PasswordHash = dto.NewPassword;

            await _userRepo.UpdateUserAsync(user);

            return NoContent();
        }
    }
}
