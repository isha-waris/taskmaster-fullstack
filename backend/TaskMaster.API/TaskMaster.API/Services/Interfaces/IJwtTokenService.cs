using TaskMaster.API.Entities;

namespace TaskMaster.API.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
