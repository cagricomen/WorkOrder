using System;

namespace WorkOrder.Core.DTOs
{
    public class WorkOrdersDto
    {
        public int Id { get; set; }
        public string WorkPlace { get; set; }
        public string ManagerName { get; set; }
        public string Job { get; set; }
        public string Department { get; set; }
        public string User { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfWaiting { get; set; }
        public DateTime DateOfFinish { get; set; }
        public string Result { get; set; }
        public string CaseType { get; set; }
        public string UserName { get; set; }
    }
}
