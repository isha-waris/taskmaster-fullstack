using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TaskMaster.API.Data;
using TaskMaster.API.Entities;
using TaskMaster.API.Repositories.Interfaces;

namespace TaskMaster.API.Repositories.Concrete
{
    public class TaskStatusHistoryRepository : ITaskStatusHistoryRepository
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
        {
            var result = await _context.TaskStatusHistories.FindAsync(id).ConfigureAwait(false);
            if (result == null)
            {
                throw new InvalidOperationException($"TaskStatusHistory with ID {id} not found.");
            }
            return result;
        }
        public async Task<TaskStatusHistory?> AddTaskStatusHistoryAsync(TaskStatusHistory tsh)
        {
            await _context.TaskStatusHistories.AddAsync(tsh).ConfigureAwait(false);
            await _context.SaveChangesAsync();
            return tsh;
        }
    }
}
