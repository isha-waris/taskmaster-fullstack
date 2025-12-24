namespace TaskMaster.API.Entities;

public class User
{
    // Primary Key
    public int Id { get; set; }

    // Basic Information
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Role { get; set; } = null!;

    // Audit
    public DateTime CreatedAt { get; set; }

    // 🔗 Navigation Properties

    // One User → Many Tasks (Assigned Tasks)
    public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();

    // One User → Many Status Changes
    public ICollection<TaskStatusHistory> StatusHistories { get; set; } = new List<TaskStatusHistory>();
}
