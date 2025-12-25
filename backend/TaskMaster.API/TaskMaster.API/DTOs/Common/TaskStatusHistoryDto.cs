namespace TaskMaster.API.DTOs.Common
{
    public class TaskStatusHistoryDto
    {
        public string OldStatus { get; set; } = null!;
        public string NewStatus { get; set; } = null!;
        public DateTime ChangedAt { get; set; }
        public int ChangedByUserId { get; set; }
    }
}
