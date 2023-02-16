using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.DAL.Entities
{
    public class TaskComments
    {
        [Key]
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public byte CommentType { get; set; }
        public byte[] Content { get; set; }
    }
}
