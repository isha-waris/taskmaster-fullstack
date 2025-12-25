using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TaskMaster.API.Data;
using TaskMaster.API.Entities;
using TaskMaster.API.Repositories.Interfaces;

namespace TaskMaster.API.Repositories.Concrete
{
    public class TaskStatusHistoryRepository :ITaskStatusHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskStatusHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TaskStatusHistory>> GetAllTaskStatusHistoryAsync()
        {
            return await _context.TaskStatusHistories.ToListAsync();
        }
        public async Task<TaskStatusHistory> GetTaskStatusHistoryByIdAsync(int id)
            => await _context.TaskStatusHistories.FindAsync(id);
        public async Task<TaskStatusHistory?> AddTaskStatusHistoryAsync(TaskStatusHistory tsh)
        {
            _context.TaskStatusHistories.AddAsync(tsh);
            await _context.SaveChangesAsync();
            return tsh;
        }
    }
}
