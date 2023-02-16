using TimeManager.BLL.DTO;

namespace TimeManager.BLL.Interfaces
{
    public interface IProjectService
    {
        void AddProject(ProjectDTO project);
        void RemoveProject(Guid id);
        void UpdateProject(ProjectDTO project);
        IEnumerable<ProjectTaskDTO> GetAllTasks(Guid projectId);
        IEnumerable<ProjectDTO> GetAll();
        public ProjectDTO GetProject(Guid Id);
        void Dispose();
    }
}
