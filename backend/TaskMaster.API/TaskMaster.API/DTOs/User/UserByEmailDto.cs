using TaskMaster.API.DTOs.Common;

namespace TaskMaster.API.DTOs.User
{
    public class UserByEmailDto
    {
        public int Id { get; set; }
        public string? Fullname { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Password { get; set; } = null!;
        public List<TaskStatusHistoryDto> StatusHistory { get; set; } = [];
    }
}
