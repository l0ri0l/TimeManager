using TimeManager.DAL.EF;
using TimeManager.DAL.Entities;
using TimeManager.DAL.Interfaces;

namespace TimeManager.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private TasksContext db;

        private ProjectRepository projectRepository;
        private ProjectTaskRepository projectTaskRepository;
        private TaskCommentsRepository taskCommentsRepository;


        public EFUnitOfWork(string connetionString)
        {
            db = new TasksContext(connetionString);
        }

        public IRepository<Project> Projects
        {
            get
            {
                projectRepository ??= new ProjectRepository(db);
                return projectRepository;
            }
        }

        public IRepository<ProjectTask> Tasks
        {
            get
            {
                if (projectTaskRepository == null)
                {
                    projectTaskRepository = new ProjectTaskRepository(db);
                }
                return projectTaskRepository;
            }
        }

        public IRepository<TaskComments> TaskComments
        {
            get
            {
                taskCommentsRepository ??= new TaskCommentsRepository(db);
                return taskCommentsRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
