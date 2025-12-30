namespace TaskMaster.API.DTOs.TaskStatusHistory
{
    public class TaskStatusHistoryResponseDto
    {
        public int Id { get; set; }
        public int TaskItemId { get; set; }
        public int OldStatus { get; set; }
        public int NewStatus { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
