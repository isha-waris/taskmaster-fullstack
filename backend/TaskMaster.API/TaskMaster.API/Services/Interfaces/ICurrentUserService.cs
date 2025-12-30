using TaskMaster.API.Enums;

namespace TaskMaster.API.Services.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        string Email { get; }
        UserRole Role { get; }
    }
}
