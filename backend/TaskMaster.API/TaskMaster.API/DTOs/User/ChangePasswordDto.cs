using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskMaster.API.DTOs.User
{
    public class ChangePasswordDto
    {
        [Required, MinLength(6), DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = null!;

        [Required, MinLength(6), DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;
    }
}