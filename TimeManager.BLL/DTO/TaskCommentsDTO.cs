
namespace TimeManager.BLL.DTO
{
    public class TaskCommentsDTO
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public byte CommentType { get; set; }
        public string? Content { get; set; }
    }
}
