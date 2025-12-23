namespace TaskMaster.API.Entities;

public class User
{
    // Primary Key
    public int Id { get; set; }

    // Basic Information
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string Role { get; set; }

    // Audit
    public DateTime CreatedAt { get; set; }

    // 🔗 Navigation Properties

    // One User → Many Tasks (Assigned Tasks)
    public required ICollection<TaskItem> AssignedTasks { get; set; }

    // One User → Many Status Changes
    public required ICollection<TaskStatusHistory> StatusHistories { get; set; }
}
