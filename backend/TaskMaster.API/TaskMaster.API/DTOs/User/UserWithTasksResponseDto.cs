namespace TaskMaster.API.DTOs.User;

public class UserWithTasksResponseDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    public List<UserTaskDto> AssignedTasks { get; set; }
}
