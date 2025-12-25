namespace TaskMaster.API.DTOs.Task
{
    public class TaskListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int AssignedToUserId { get; set; }
    }
}
