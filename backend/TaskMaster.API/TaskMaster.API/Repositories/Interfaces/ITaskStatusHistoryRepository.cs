using TaskMaster.API.Entities;

namespace TaskMaster.API.Repositories.Interfaces
{
    public interface ITaskStatusHistoryRepository
    {
        Task<IEnumerable<TaskStatusHistory>> GetAllTaskStatusHistoryAsync();
        Task<TaskStatusHistory> GetTaskStatusHistoryByIdAsync(int id);
        Task<TaskStatusHistory?> AddTaskStatusHistoryAsync(TaskStatusHistory tsh);
    }
}
