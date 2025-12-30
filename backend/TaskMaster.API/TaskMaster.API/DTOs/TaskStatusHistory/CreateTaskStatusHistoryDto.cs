using System.ComponentModel.DataAnnotations;

namespace TaskMaster.API.DTOs.TaskStatusHistory
{
    public class CreateTaskStatusHistoryDto
    {
        [Required] public int TaskItemId { get; set; }
        [Required, StringLength(32)] public int OldStatus { get; set; } 
        [Required, StringLength(32)] public int NewStatus { get; set; } 
        [Required] public int ChangedByUserId { get; set; }
    }
}
