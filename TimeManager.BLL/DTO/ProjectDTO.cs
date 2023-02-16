using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.BLL.DTO
{
    public class ProjectDTO
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
