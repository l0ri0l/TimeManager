using TimeManager.DAL.Entities;

namespace TimeManager.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Project> Projects { get; }
        IRepository<ProjectTask> Tasks { get; }
        IRepository<TaskComments> TaskComments { get; }
        void Save();
    }
}
