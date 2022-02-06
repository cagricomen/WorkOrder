using System;

namespace WorkOrder.Core.Entities
{
    public class WorkOrders
    {
        public int Id { get; set; }
        public int WorkPlaceId { get; set; }
        public WorkPlace WorkPlace { get; set; }
        public string ManagerName { get; set; }
        public string Job { get; set; }
        public int DepartmentId { get; set; }
        public Department Department{ get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime DateOfOrder { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfWaiting { get; set; }
        public DateTime DateOfFinish{ get; set; }
        public string Result{ get; set; }
        public int CaseTypeId { get; set; }
        public CaseType CaseType { get; set; }
    }
}
