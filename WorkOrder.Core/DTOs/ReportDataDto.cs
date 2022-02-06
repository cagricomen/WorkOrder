namespace WorkOrder.Core.DTOs
{
    public class ReportDataDto
    {
        public int AllWork { get; set; }
        public int RequestWork { get; set; }
        public int DoneWork { get; set; }
        public int WaitingWork { get; set; }
        public int OrderWork { get; set; }
        public int DoingWork { get; set; }
        public int CanselWork { get; set; }
    }
}
