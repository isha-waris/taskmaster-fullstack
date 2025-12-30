namespace TaskMaster.API.DTOs.User
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Role { get; set; } 
    }
}
