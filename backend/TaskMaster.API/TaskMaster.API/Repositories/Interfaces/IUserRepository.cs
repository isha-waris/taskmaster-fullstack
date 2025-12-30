using TaskMaster.API.Entities;

namespace TaskMaster.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> AddUserAsync(User user);
        Task<User?> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> ChangePasswordAsync(int id, string newPasswordHash);
    }
}
//An enumeration (or enum) in C# is a value type defined by a set of named constants of the underlying integral numeric type.
//Enums are used to represent a choice from a set of mutually exclusive values or a combination of choices. They make the code more readable
//and maintainable by providing meaningful names to integral constants.
