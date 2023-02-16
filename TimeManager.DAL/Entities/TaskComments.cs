using System.ComponentModel.DataAnnotations;

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
