using System.ComponentModel.DataAnnotations;

namespace TaskMaster.API.DTOs.User
{
        public class UpdateUserDto
        {
            [Required, StringLength(100)]
            public string FullName { get; set; } = null!;

            [Required, EmailAddress, StringLength(200)]
            public string Email { get; set; } = null!;

            [Required, StringLength(50)]
            public string Role { get; set; } = null!;
        }
}
