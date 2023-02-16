using System;

namespace TimeManager.WEB.Models
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
