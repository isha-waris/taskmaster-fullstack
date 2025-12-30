using System.ComponentModel.DataAnnotations;

namespace TaskMaster.API.DTOs.Task
{
    public class ChangeTaskStatusDto
    {
        [Required, StringLength(32)] public TaskStatus NewStatus { get; set; }
        [Required] public int ChangedByUserId { get; set; }
    }
}
