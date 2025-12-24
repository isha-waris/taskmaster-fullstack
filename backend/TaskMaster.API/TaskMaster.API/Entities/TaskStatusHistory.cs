namespace TaskMaster.API.Entities;

public class TaskStatusHistory
{
    // Primary Key
    public  int Id { get; set; }

    // Foreign Key → Task
    public int TaskItemId { get; set; }

    // Status Tracking
    public string OldStatus { get; set; } = null!;
    public  string NewStatus { get; set; } = null!;

    // Audit
    public DateTime ChangedAt { get; set; }

    // Foreign Key → User
    public  int ChangedByUserId { get; set; }

    // 🔗 Navigation Properties

    // Many History Records → One Task
    public TaskItem TaskItem { get; set; } = null!;

    // Many History Records → One User
    public User ChangedByUser { get; set; } = null!;
}
