using TaskMaster.API.DTOs.Common;

namespace TaskMaster.API.DTOs.User
{
    public class UserTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<TaskStatusHistoryDto> StatusHistory { get; set; } = [];
    }
}
