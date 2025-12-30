using System.ComponentModel.DataAnnotations;

namespace TaskMaster.API.DTOs.Authorisation
{
    public class LoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
