using System.ComponentModel.DataAnnotations;

namespace TaskMaster.API.DTOs.Task
{
    public class ChangeTaskStatusDto
    {
        [Required, StringLength(32)] public string NewStatus { get; set; } = null!;
        [Required] public int ChangedByUserId { get; set; }
    }
}
