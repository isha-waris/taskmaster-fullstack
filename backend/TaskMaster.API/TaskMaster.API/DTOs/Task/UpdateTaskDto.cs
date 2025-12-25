using System.ComponentModel.DataAnnotations;

namespace TaskMaster.API.DTOs.Task
{
    public class UpdateTaskDto
    {
        [Required, StringLength(150)] public string Title { get; set; } = null!;
        [Required, StringLength(2000)] public string Description { get; set; } = null!;
        [Required] public int AssignedToUserId { get; set; }
        [Required, StringLength(32)] public string Status { get; set; } = null!;
    }
}
