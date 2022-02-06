using WorkOrder.Core.Entities;

namespace WorkOrder.Core.DTOs
{
    public class UserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole UserRole { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
    }
}
