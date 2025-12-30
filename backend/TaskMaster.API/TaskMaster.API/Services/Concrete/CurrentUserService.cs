using System.Security.Claims;
using TaskMaster.API.Enums;
using TaskMaster.API.Services.Interfaces;

namespace TaskMaster.API.Services.Concrete
{
    public class CurrentUserService: ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int UserId =>
         int.Parse(_httpContextAccessor.HttpContext?
             .User?
             .FindFirstValue(ClaimTypes.NameIdentifier)!);

        public string Email =>
            _httpContextAccessor.HttpContext?
                .User?
                .FindFirstValue(ClaimTypes.Email)!;

        public UserRole Role =>
            Enum.Parse<UserRole>(_httpContextAccessor.HttpContext?
                .User?
                .FindFirstValue(ClaimTypes.Role)!);

    }
}
