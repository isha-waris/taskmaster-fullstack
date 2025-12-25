using System.ComponentModel.DataAnnotations;

namespace TaskMaster.API.DTOs.TaskStatusHistory
{
    public class CreateTaskStatusHistoryDto
    {
        [Required] public int TaskItemId { get; set; }
        [Required, StringLength(32)] public string OldStatus { get; set; } = null!;
        [Required, StringLength(32)] public string NewStatus { get; set; } = null!;
        [Required] public int ChangedByUserId { get; set; }
    }
}
