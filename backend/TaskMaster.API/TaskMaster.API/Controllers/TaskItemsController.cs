using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Reflection;
using TaskMaster.API.DTOs.Task;
using TaskMaster.API.DTOs.Common;
using TaskMaster.API.Entities;
using TaskMaster.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TaskMaster.API.DTOs.User;

namespace TaskMaster.API.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class TaskItemsController : ControllerBase
    {
        private readonly ITaskRepository _taskRepo;
        public TaskItemsController(ITaskRepository taskRepo)
        {
            _taskRepo = taskRepo;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var task = await _taskRepo.GetAllTasksAsync();
            if (task is null) return NotFound();
            var result = task.Select(t=> new TaskListDTO
            {
                Id=t.Id,
                Title=t.Title,
                Status=t.Status,
                AssignedToUserId=t.AssignedToUserId
            });
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskRepo.GetTaskByIdAsync(id);
            if (task == null) return NotFound();

            var response = new TaskWithHistoryResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                AssignedToUserId = task.AssignedToUserId,
                CreatedAt = task.CreatedAt,

                StatusHistory = task.StatusHistory.Select(h => new TaskStatusHistoryDto
                {
                    OldStatus = h.OldStatus.ToString(),
                    NewStatus = h.NewStatus.ToString(),
                    ChangedAt = h.ChangedAt,
                    ChangedByUserId = h.ChangedByUserId
                }).ToList()
            };

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(TaskResponseDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status.ToString(), // Convert TaskStatus to string
                AssignedToUserId = dto.AssignedToUserId
            };
            await _taskRepo.AddTaskAsync(task);
            var taskResponseDto = new TaskResponseDto
            {
                Id=task.Id,
                Title=task.Title,
                Description=task.Description,
                Status=task.Status,
                AssignedToUserId=task.AssignedToUserId,
                CreatedAt=task.CreatedAt
            };
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, taskResponseDto);
        }

        [Authorize]
        [HttpPost("createmytasks")]
        [ProducesResponseType(typeof(TaskResponseDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateMyTaks(CreateTaskDto dto)
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status.ToString(), // Convert TaskStatus to string
                AssignedToUserId = userId, // 👈 FROM TOKEN
                CreatedAt = DateTime.UtcNow
            };

            await _taskRepo.AddTaskAsync(task);
            return Ok();
        }

        [Authorize]
        [HttpGet("mytasks")]
        public async Task<IActionResult> GetMyTasks()
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var tasks = await _taskRepo.GetTaskByIdAsync(userId);
            return Ok(tasks);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(TaskResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto dto)
        {
            var existing = await _taskRepo.GetTaskByIdAsync(id);
            if (existing is null) return NotFound();
            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Status = dto.Status.ToString(); // Ensure TaskStatus is converted to string
            var updated = await _taskRepo.UpdateTaskAsync(existing);
            if (updated is null) return NotFound();

            var result = new UpdateTaskDto
            {
                AssignedToUserId = updated.AssignedToUserId,
                Description = updated.Description,
                Status = Enum.Parse<TaskStatus>(value: updated.Status), // Convert string back to TaskStatus
                Title = updated.Title,
            };
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var ok = await _taskRepo.DeleteTaskAsync(id);
            return ok? NoContent(): NotFound();

        }
    }
}
