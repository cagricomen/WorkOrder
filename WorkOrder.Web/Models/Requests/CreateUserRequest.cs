

namespace WorkOrder.Web.Models
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int UserRoleId { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
    }
    
}
