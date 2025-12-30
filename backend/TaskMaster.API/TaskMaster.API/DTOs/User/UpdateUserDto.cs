using System.ComponentModel.DataAnnotations;
using TaskMaster.API.Enums;

namespace TaskMaster.API.DTOs.User
{
        public class UpdateUserDto
        {
            [Required, StringLength(100)]
            public string FullName { get; set; } = null!;

            [Required, EmailAddress, StringLength(200)]
            public string Email { get; set; } = null!;

            [Required, StringLength(50)]
            public UserRole Role { get; set; } 
        }
}
