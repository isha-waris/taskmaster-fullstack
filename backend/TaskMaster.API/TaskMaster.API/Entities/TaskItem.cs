namespace TaskMaster.API.Entities;

public class TaskItem
{
    // Primary Key
    public int Id { get; set; }

    // Task Details
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Status { get; set; }

    // Foreign Key → User
    public int AssignedToUserId { get; set; }

    // Audit
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // 🔗 Navigation Properties

    // Many Tasks → One User
    public required User AssignedToUser { get; set; }

    // One Task → Many Status History Records
    public required ICollection<TaskStatusHistory> StatusHistory { get; set; }
}
