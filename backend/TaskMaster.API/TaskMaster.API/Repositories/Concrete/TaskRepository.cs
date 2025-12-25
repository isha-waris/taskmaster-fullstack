using Microsoft.EntityFrameworkCore;
using TaskMaster.API.Data;
using TaskMaster.API.Entities;
using TaskMaster.API.Repositories.Interfaces;

namespace TaskMaster.API.Repositories.Concrete
{
    public class TaskRepository : ITaskRepository   
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
            => await _context.TaskItems.ToListAsync();
        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _context.TaskItems
                .Include(t => t.StatusHistory)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TaskItem> AddTaskAsync(TaskItem task)
        {
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }
        public async Task<TaskItem?> UpdateTaskAsync(TaskItem task)
        {
            var existingTask = await _context.TaskItems.FindAsync(task.Id);
            if (existingTask == null) return null;
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            existingTask.AssignedToUserId = task.AssignedToUserId;
            existingTask.UpdatedAt = DateTime.UtcNow;
            existingTask.StatusHistory = task.StatusHistory;
            await _context.SaveChangesAsync();
            return existingTask;
        }
        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null) return false;
            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
