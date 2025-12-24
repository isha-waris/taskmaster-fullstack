namespace TaskMaster.API.Entities;

public class TaskItem
{
    // Primary Key
    public  int Id { get; set; }

    // Task Details
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Status { get; set; } = null!;

    // Foreign Key → User
    public  int AssignedToUserId { get; set; }

    // Audit
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // 🔗 Navigation Properties

    // Many Tasks → One User
    public User AssignedToUser { get; set; } = null!;

    // One Task → Many Status History Records
    public ICollection<TaskStatusHistory> StatusHistory { get; set; } = new List<TaskStatusHistory>();
}
