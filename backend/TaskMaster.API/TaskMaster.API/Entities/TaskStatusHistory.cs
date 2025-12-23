namespace TaskMaster.API.Entities;

public class TaskStatusHistory
{
    // Primary Key
    public int Id { get; set; }

    // Foreign Key → Task
    public int TaskItemId { get; set; }

    // Status Tracking
    public required string OldStatus { get; set; }
    public required string NewStatus { get; set; }

    // Audit
    public DateTime ChangedAt { get; set; }

    // Foreign Key → User
    public int ChangedByUserId { get; set; }

    // 🔗 Navigation Properties

    // Many History Records → One Task
    public required TaskItem TaskItem { get; set; }

    // Many History Records → One User
    public required User ChangedByUser { get; set; }
}
