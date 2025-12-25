namespace TaskMaster.API.DTOs.TaskStatusHistory
{
    public class TaskStatusHistoryResponseDto
    {
        public int Id { get; set; }
        public int TaskItemId { get; set; }
        public string OldStatus { get; set; } = null!;
        public string NewStatus { get; set; } = null!;
        public DateTime ChangedAt { get; set; }
    }
}
