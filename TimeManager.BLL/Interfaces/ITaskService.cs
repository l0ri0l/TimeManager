using TimeManager.BLL.DTO;

namespace TimeManager.BLL.Interfaces
{
    public interface ITaskService
    {
        public void CreateTask(ProjectTaskDTO task, Guid ProjectId);
        public TaskCommentsDTO GetComment(Guid Id);
        public IEnumerable<TaskCommentsDTO> GetComments(Guid taskId);
        public void RemoveTask(Guid Id);
        public void StopTask(Guid Id);
        public void CancelTask(Guid Id);
        public void StartTask(Guid Id);
        public ProjectTaskDTO FindTask(Guid Id);
        public void Dispose();
        public void AddComment(TaskCommentsDTO commentsDTO, Guid taskId);
        public void EditComment(TaskCommentsDTO commentDTO);
        public void RemoveComment(Guid commentId);
    }
}
