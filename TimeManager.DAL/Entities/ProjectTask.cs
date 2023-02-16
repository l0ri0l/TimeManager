using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.DAL.Entities
{
    public class ProjectTask
    {
        [Key]
        public Guid Id { get; set; }
        public string TaskName { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CancelDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
