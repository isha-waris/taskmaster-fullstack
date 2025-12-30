namespace TaskMaster.API.DTOs.User;

public class UserWithTasksResponseDto
{
    public int Id { get; set; }
    public string? FullName { get; set; } = null;
    public string? Email { get; set; } = null;
    public int Role { get; set; }

    public required List<UserTaskDto> AssignedTasks { get; set; } = [];
}
