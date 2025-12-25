using TaskMaster.API.DTOs.Common;
namespace TaskMaster.API.DTOs.Task
{
    public class TaskWithHistoryResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int AssignedToUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<TaskStatusHistoryDto> StatusHistory { get; set; } = new List<TaskStatusHistoryDto>();
    }

}
