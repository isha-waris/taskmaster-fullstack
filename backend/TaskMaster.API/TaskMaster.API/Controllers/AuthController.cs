using Microsoft.AspNetCore.Mvc;
using TaskMaster.API.DTOs.Authorisation;
using TaskMaster.API.Repositories.Interfaces;
using TaskMaster.API.Services.Interfaces;

namespace TaskMaster.API.Controllers
{
    [ApiController] // Add this attribute to make the controller work properly with ASP.NET Core
    [Route("api/[controller]")] // Define the route for the controller
    public class AuthController : ControllerBase // Change base class to ControllerBase to use built-in methods like Unauthorized
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtTokenService _tokenService;

        public AuthController(IUserRepository userRepo, IJwtTokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userRepo.GetUserByEmailAsync(dto.Email);
            if (user is null) return Unauthorized("Invalid credentials");

            if (user.PasswordHash != dto.Password) // replace with hash later
                return Unauthorized("Invalid credentials");

            var token = _tokenService.GenerateToken(user);

            return Ok(new
            {
                token,
                user.Id,
                user.FullName,
                user.Role
            });
        }
    }
}

