using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Reflection;
using TaskMaster.API.DTOs.Task;
using TaskMaster.API.DTOs.Common;
using TaskMaster.API.Entities;
using TaskMaster.API.Repositories.Interfaces;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskListDTO>>> GetAll()
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
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
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,[FromBody] UpdateTaskDto dto)
        {
            var existing = await _taskRepo.GetTaskByIdAsync(id);
            if (existing is null) return NotFound();
            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Status = dto.Status;
            var updated= await _taskRepo.UpdateTaskAsync(existing);
            if (updated is null) return NotFound();

            var result = new UpdateTaskDto
            {
                AssignedToUserId = updated.AssignedToUserId,
                Description= updated.Description,
                Status=updated.Status,
                Title=updated.Title,
            };
            return Ok(result);

        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ok = await _taskRepo.DeleteTaskAsync(id);
            return ok? NoContent(): NotFound();

        }
    }
}
