using TimeManager.DAL.EF;
using TimeManager.DAL.Entities;
using TimeManager.DAL.Interfaces;

namespace TimeManager.DAL.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly TasksContext db;

        public ProjectRepository(TasksContext context)
        {
            this.db = context;
        }

        public void Create(Project item)
        {
            db.Projects.Add(item);
        }

        public void Delete(Guid Id)
        {
            Project project = db.Projects.Find(Id);
            if(project != null)
            {
                db.Projects.Remove(project);
            }
        }

        public IEnumerable<Project> Find(Func<Project, bool> predicate)
        {
            return db.Projects.Where(predicate).ToList();
        }

        public Project Get(Guid Id)
        {
            return db.Projects.Find(Id);
        }

        public IEnumerable<Project> GetAll()
        {
            return db.Projects;
        }

        public void Update(Project item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
