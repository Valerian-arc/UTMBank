using Domain.Enums;

namespace Core.DTOs.Admin
{
    public class UserEditDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public UserRole Role { get; set; }
    }
}
