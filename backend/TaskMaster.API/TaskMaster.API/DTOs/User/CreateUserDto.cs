using System.ComponentModel.DataAnnotations;
using TaskMaster.API.Enums;

namespace TaskMaster.API.DTOs.User
{
    public class CreateUserDto
    {
        [Required]
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(6)]
        public string PasswordHash { get; set; }
        [Required]
        public UserRole Role { get; set; }
    }
}
