namespace TimeManager.WEB.Models
{
    public class TaskCommentsViewModel
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public byte CommentType { get; set; }
        public string? Content { get; set; }
    }
}
