using TimeManager.DAL.EF;
using TimeManager.DAL.Entities;
using TimeManager.DAL.Interfaces;

namespace TimeManager.DAL.Repositories
{
    public class ProjectTaskRepository : IRepository<ProjectTask>
    {
        private readonly TasksContext db;
        
        public ProjectTaskRepository(TasksContext context)
        {
            this.db = context;
        }

        public void Create(ProjectTask item)
        {
            db.Tasks.Add(item);
        }

        public void Delete(Guid Id)
        {
            ProjectTask task = db.Tasks.Find(Id);
            if (task != null)
            {
                db.Tasks.Remove(task);
            }
        }

        public IEnumerable<ProjectTask> Find(Func<ProjectTask, bool> predicate)
        {
            return db.Tasks.Where(predicate).ToList();
        }

        public ProjectTask Get(Guid Id)
        {
            return db.Tasks.Find(Id);
        }

        public IEnumerable<ProjectTask> GetAll()
        {
            return db.Tasks;
        }

        public void Update(ProjectTask item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
