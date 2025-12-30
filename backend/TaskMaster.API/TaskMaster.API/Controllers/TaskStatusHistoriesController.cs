using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.API.DTOs.TaskStatusHistory;
using TaskMaster.API.Entities;
using TaskMaster.API.Repositories.Interfaces;

namespace TaskMaster.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskStatusHistoriesController : ControllerBase
{
    private readonly ITaskStatusHistoryRepository _historyRepo;

    public TaskStatusHistoriesController(ITaskStatusHistoryRepository historyRepo)
    {
        _historyRepo = historyRepo;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var history = await _historyRepo.GetAllTaskStatusHistoryAsync();
        return Ok(history);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskStatusHistoryDto dto)
    {
        var history = new TaskStatusHistory
        {
            TaskItemId = dto.TaskItemId,
            OldStatus = dto.OldStatus,
            NewStatus = dto.NewStatus,
            ChangedByUserId = dto.ChangedByUserId,
            ChangedAt = DateTime.UtcNow
        };

        await _historyRepo.AddTaskStatusHistoryAsync(history);

        var response = new TaskStatusHistoryResponseDto
        {
            Id = history.Id,
            TaskItemId = history.TaskItemId,
            OldStatus = history.OldStatus,
            NewStatus = history.NewStatus,
            ChangedAt = history.ChangedAt
        };

        return Ok(response);
    }
}
