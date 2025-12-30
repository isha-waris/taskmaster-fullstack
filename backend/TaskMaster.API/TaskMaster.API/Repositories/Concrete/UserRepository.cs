using Microsoft.EntityFrameworkCore;
using TaskMaster.API.Data;
using TaskMaster.API.Entities;
using TaskMaster.API.Repositories.Interfaces;

namespace TaskMaster.API.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
            => await _context.Users.ToListAsync();

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.AssignedTasks)
                    .ThenInclude(t => t.StatusHistory)
                .Include(u => u.StatusHistories)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
               .Include(u => u.AssignedTasks)
                   .ThenInclude(t => t.StatusHistory)
               .Include(u => u.StatusHistories)
               .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null) return null;

            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool>  ChangePasswordAsync(int id, string newPasswordHash)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null) return false;
            user.PasswordHash = newPasswordHash;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
