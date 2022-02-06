using System;

namespace WorkOrder.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole UserRole { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
    }
    public enum UserRole
    {
        Admin = 0,
        Manager = 1,
        User = 2,
    }
}
