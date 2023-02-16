using System;

namespace TimeManager.WEB.Models
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public string TaskName { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime CancelTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
